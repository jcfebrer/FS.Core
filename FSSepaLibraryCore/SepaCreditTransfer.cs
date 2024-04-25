using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using FSSepaLibraryCore.Utils;
using static FSSepaLibraryCore.SepaInstructionForCreditor;

namespace FSSepaLibraryCore
{
    /// <summary>
    ///     Manage SEPA (Single Euro Payments Area) CreditTransfer for SEPA or international order.
    ///     Only one PaymentInformation is managed but it can manage multiple transactions.
    /// </summary>
    public class SepaCreditTransfer : SepaTransfer<SepaCreditTransferTransaction>
    {
        /// <summary>
        ///     Debtor account ISO currency code (default is EUR)
        /// </summary>
        public string DebtorAccountCurrency { get; set; }

        /// <summary>
        ///     Is it an international credit transfer?
        /// </summary>
        public bool IsInternational { get; set; }


        /// <summary>
        ///     Charger bearer for international credit transfer
        /// </summary>
        public SepaChargeBearer ChargeBearer { get; set; }

        /// <summary>
        /// Create a Sepa Credit Transfer using Pain.001.001.03 schema
        /// </summary>
        public SepaCreditTransfer()
        {
            DebtorAccountCurrency = Constant.EuroCurrency;
            schema = SepaSchema.Pain00100103;
            IsInternational = false;
            ChargeBearer = SepaChargeBearer.DEBT;
        }

        /// <summary>
        /// Create a Sepa Debit Transfer using Pain.001.001.03 schema
        /// </summary>
        public SepaCreditTransfer(string xmlFile)
        {
            DebtorAccountCurrency = Constant.EuroCurrency;
            schema = SepaSchema.Pain00100103;
            IsInternational = false;
            ChargeBearer = SepaChargeBearer.DEBT;

            LoadXml(xmlFile);
        }

        /// <summary>
        ///     Debtor IBAN data
        /// </summary>
        /// <exception cref="SepaRuleException">If debtor to set is not valid.</exception>
        public SepaIbanData Debtor
        {
            get { return SepaIban; }
            set
            {
                //if (!value.IsValid || value.UnknownBic)
                //    throw new SepaRuleException("Debtor IBAN data are invalid.");
                SepaIban = value;
            }
        }

        /// <summary>
        ///     Is Mandatory data are set ? In other case a SepaRuleException will be thrown
        /// </summary>
        /// <exception cref="SepaRuleException">If mandatory data is missing.</exception>
        protected override void CheckMandatoryData()
        {
            base.CheckMandatoryData();

            if (Debtor == null)
            {
                throw new SepaRuleException("The debtor is mandatory.");
            }
        }

        /// <summary>
        ///     Add an existing transfer transaction
        /// </summary>
        /// <param name="transfer"></param>
        /// <exception cref="ArgumentNullException">If transfert is null.</exception>
        public void AddCreditTransfer(SepaCreditTransferTransaction transfer)
        {
            AddTransfer(transfer);
        }

        /// <summary>
        ///     Carga el fichero xml
        /// </summary>
        /// <returns></returns>
        protected override void LoadXml(string xmlFile)
        {
            var xmlReader = new XmlTextReader(xmlFile);
            xmlReader.Namespaces = false;
            var xml = new XmlDocument();
            xml.Load(xmlReader);

            // Part 1: Group Header
            var cstmrCdtTrfInitn = XmlUtils.GetFirstElement(xml, "CstmrCdtTrfInitn");
            var grpHdr = XmlUtils.GetFirstElement(cstmrCdtTrfInitn, "GrpHdr");
            MessageIdentification = XmlUtils.GetFirstElement(grpHdr, "MsgId").InnerText;
            CreationDate = Convert.ToDateTime(XmlUtils.GetFirstElement(grpHdr, "CreDtTm").InnerText);
            numberOfTransactions = Convert.ToInt32(XmlUtils.GetFirstElement(grpHdr, "NbOfTxs").InnerText);
            headerControlSum = Convert.ToDecimal(XmlUtils.GetFirstElement(grpHdr, "CtrlSum").InnerText.Replace(".",","));

            var initgPty = XmlUtils.GetFirstElement(grpHdr, "InitgPty");
            InitiatingPartyName = XmlUtils.GetFirstElement(initgPty, "Nm").InnerText;
            InitiatingPartyId = XmlUtils.GetFirstElement(initgPty, "Id/OrgId/Othr/Id").InnerText;

            // Part 2: Payment Information
            var pmtInf = XmlUtils.GetFirstElement(xml, "CstmrCdtTrfInitn/PmtInf");
            PaymentInfoId = XmlUtils.GetFirstElement(pmtInf, "PmtInfId").InnerText;
            if (PaymentInfoId == null)
                PaymentInfoId = MessageIdentification;

            //Constant.CreditTransfertPaymentMethod = XmlUtils.GetFirstElement(pmtInf, "PmtMtd").InnerText;

            numberOfTransactions = Convert.ToInt32(XmlUtils.GetFirstElement(pmtInf, "NbOfTxs").InnerText);
            paymentControlSum = Convert.ToDecimal(XmlUtils.GetFirstElement(pmtInf, "CtrlSum").InnerText);

            var pmtTpInf = XmlUtils.GetFirstElement(pmtInf, "PmtTpInf");
            if (XmlUtils.GetFirstElement(pmtTpInf, "InstrPrty").InnerText == "NORM")
                IsInternational = true;

            if (XmlUtils.GetFirstElement(pmtTpInf, "SvcLvl/Cd").InnerText == "SEPA")
                IsInternational = false;

            if(XmlUtils.GetFirstElement(pmtTpInf, "LclInstr/Cd")!=null)
                LocalInstrumentCode = XmlUtils.GetFirstElement(pmtTpInf, "LclInstr/Cd").InnerText;

            CategoryPurposeCode = XmlUtils.GetFirstElement(pmtTpInf, "CtgyPurp/Cd").InnerText;
            RequestedExecutionDate = Convert.ToDateTime(XmlUtils.GetFirstElement(pmtInf, "ReqdExctnDt").InnerText);

            var dbtr = XmlUtils.GetFirstElement(pmtInf, "Dbtr");

            Debtor = new SepaIbanData();
            Debtor.Address = new SepaPostalAddress();
            Debtor.Name = XmlUtils.GetFirstElement(dbtr, "Nm").InnerText;

            ReadSepaPostalAddress(dbtr, Debtor.Address);

            if (XmlUtils.GetFirstElement(pmtInf, "Dbtr/Id/OrgId/Othr/Id") != null)
                InitiatingPartyId = XmlUtils.GetFirstElement(pmtInf, "Dbtr/Id/OrgId/Othr/Id").InnerText;

            if (XmlUtils.GetFirstElement(pmtInf, "DbtrAcct/Id/IBAN") != null)
                Debtor.Iban = XmlUtils.GetFirstElement(pmtInf, "DbtrAcct/Id/IBAN").InnerText;

            if (XmlUtils.GetFirstElement(pmtInf, "DbtrAcct/Id/IBAN/Ccy") != null)
                DebtorAccountCurrency = XmlUtils.GetFirstElement(pmtInf, "DbtrAcct/Id/IBAN/Ccy").InnerText;

            var finInstnId = XmlUtils.GetFirstElement(pmtInf, "DbtrAgt/FinInstnId");
            Debtor.Bic = XmlUtils.GetFirstElement(finInstnId, "BIC").InnerText;

            Debtor.AgentAddress = new SepaPostalAddress();
            ReadSepaPostalAddress(finInstnId, Debtor.AgentAddress);

            if (IsInternational)
            {
                Enum.TryParse(XmlUtils.GetFirstElement(pmtInf, "ChrgBr").InnerText, out SepaChargeBearer chargeBearer);
                ChargeBearer = chargeBearer;
            }

            // Part 3: Credit Transfer Transaction Information
            var cdtTrfTxInf = pmtInf.SelectNodes("CdtTrfTxInf");
            if (cdtTrfTxInf != null)
            {
                foreach (XmlNode node in cdtTrfTxInf)
                {
                    SepaCreditTransferTransaction transfer = new SepaCreditTransferTransaction();

                    if (XmlUtils.GetFirstElement(node, "PmtId/InstrId") != null)
                        transfer.Id = XmlUtils.GetFirstElement(node, "PmtId/InstrId").InnerText;

                    if (XmlUtils.GetFirstElement(node, "EndToEndId") != null)
                        transfer.EndToEndId = XmlUtils.GetFirstElement(node, "EndToEndId").InnerText;
                    transfer.Amount = Convert.ToDecimal(XmlUtils.GetFirstElement(node, "Amt/InstdAmt").InnerText.Replace(".", ","));
                    transfer.Currency = XmlUtils.GetFirstElement(node, "Amt/InstdAmt").GetAttribute("Ccy");

                    transfer.Creditor = new SepaIbanData();
                    var cdtrAgt = XmlUtils.GetFirstElement(node, "CdtrAgt");
                    transfer.Creditor.Bic = XmlUtils.GetFirstElement(cdtrAgt, "FinInstnId/BIC").InnerText;
                    
                    var cdtr = XmlUtils.GetFirstElement(node, "Cdtr");
                    transfer.Creditor.Name = XmlUtils.GetFirstElement(cdtr, "Nm").InnerText;

                    transfer.Creditor.Address = new SepaPostalAddress();
                    ReadSepaPostalAddress(cdtr, transfer.Creditor.Address);

                    var cdtrAcct = XmlUtils.GetFirstElement(node, "CdtrAcct");
                    transfer.Creditor.Iban = XmlUtils.GetFirstElement(cdtrAcct, "Id/IBAN").InnerText;

                    if (IsInternational)
                    {
                        var instr = XmlUtils.GetFirstElement(node, "InstrForCdtrAgt");
                        if (instr != null)
                        {
                            Enum.TryParse(XmlUtils.GetFirstElement(instr, "Cd").InnerText, out SepaInstructionForCreditorCode code);
                            transfer.SepaInstructionForCreditor.Code = code;
                            transfer.SepaInstructionForCreditor.Comment = XmlUtils.GetFirstElement(instr, "InstrInf").InnerText;
                        }
                    }

                    if(XmlUtils.GetFirstElement(node, "Purp/Cd") != null)
                        transfer.Purpose = XmlUtils.GetFirstElement(node, "Purp/Cd").InnerText;
                    
                    if (IsInternational)
                    {
                        transfer.RegulatoryReportingCode = XmlUtils.GetFirstElement(node, "RgltryRptg/Dtls/Cd").InnerText;
                    }

                    transfer.RemittanceInformation = XmlUtils.GetFirstElement(node, "RmtInf/Ustrd").InnerText;

                    transactions.Add(transfer);
                }
            }
        }


        /// <summary>
        ///     Generate the XML structure
        /// </summary>
        /// <returns></returns>
        protected override XmlDocument GenerateXml()
        {
            CheckMandatoryData();

            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            var el = (XmlElement)xml.AppendChild(xml.CreateElement("Document"));
            el.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            el.SetAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:" + SepaSchemaUtils.SepaSchemaToString(schema));
            el.NewElement("CstmrCdtTrfInitn");

            // Part 1: Group Header
            var grpHdr = XmlUtils.GetFirstElement(xml, "CstmrCdtTrfInitn").NewElement("GrpHdr");
            grpHdr.NewElement("MsgId", MessageIdentification);
            grpHdr.NewElement("CreDtTm", StringUtils.FormatDateTime(CreationDate));
            grpHdr.NewElement("NbOfTxs", numberOfTransactions);
            grpHdr.NewElement("CtrlSum", StringUtils.FormatAmount(headerControlSum));
            if (!String.IsNullOrEmpty(InitiatingPartyName) || InitiatingPartyId != null)
            {
                var initgPty = grpHdr.NewElement("InitgPty");

                if (!String.IsNullOrEmpty(InitiatingPartyName))
                {
                    initgPty.NewElement("Nm", InitiatingPartyName);
                }

                if (InitiatingPartyId != null)
                {
                    initgPty.
                        NewElement("Id").NewElement("OrgId").
                        NewElement("Othr").NewElement("Id", InitiatingPartyId);
                }
            }

            // Part 2: Payment Information
            var pmtInf = XmlUtils.GetFirstElement(xml, "CstmrCdtTrfInitn").NewElement("PmtInf");
            pmtInf.NewElement("PmtInfId", PaymentInfoId ?? MessageIdentification);

            pmtInf.NewElement("PmtMtd", Constant.CreditTransfertPaymentMethod);
            pmtInf.NewElement("NbOfTxs", numberOfTransactions);
            pmtInf.NewElement("CtrlSum", StringUtils.FormatAmount(paymentControlSum));

            if (IsInternational)
            {
                pmtInf.NewElement("PmtTpInf").NewElement("InstrPrty", "NORM");
            } else
            {
                pmtInf.NewElement("PmtTpInf").NewElement("SvcLvl").NewElement("Cd", "SEPA");
            }
            if (LocalInstrumentCode != null)
                XmlUtils.GetFirstElement(pmtInf, "PmtTpInf").NewElement("LclInstr")
                        .NewElement("Cd", LocalInstrumentCode);

			if (CategoryPurposeCode != null) {
				 XmlUtils.GetFirstElement(pmtInf, "PmtTpInf").
					 NewElement("CtgyPurp").
					 NewElement("Cd", CategoryPurposeCode);
			}
			
			pmtInf.NewElement("ReqdExctnDt", StringUtils.FormatDate(RequestedExecutionDate));
            var dbtr = pmtInf.NewElement("Dbtr");
            dbtr.NewElement("Nm", Debtor.Name);
            if (Debtor.Address != null)
            {
                AddPostalAddressElements(dbtr, Debtor.Address);
            }
			if (InitiatingPartyId != null) {
				XmlUtils.GetFirstElement(pmtInf, "Dbtr").
					NewElement("Id").NewElement("OrgId").
					NewElement("Othr").NewElement("Id", InitiatingPartyId);
			}

            var dbtrAcct = pmtInf.NewElement("DbtrAcct");
            dbtrAcct.NewElement("Id").NewElement("IBAN", Debtor.Iban);
            dbtrAcct.NewElement("Ccy", DebtorAccountCurrency);

            var finInstnId = pmtInf.NewElement("DbtrAgt").NewElement("FinInstnId");
            finInstnId.NewElement("BIC", Debtor.Bic);
            if (Debtor.AgentAddress != null)
            {
                AddPostalAddressElements(finInstnId, Debtor.AgentAddress);
            }

            if (IsInternational)
            {
                pmtInf.NewElement("ChrgBr", SepaChargeBearerUtils.SepaChargeBearerToString(ChargeBearer));
            } else
            {
                pmtInf.NewElement("ChrgBr", "SLEV");
            }

            // Part 3: Credit Transfer Transaction Information
            foreach (var transfer in transactions)
            {
                GenerateTransaction(pmtInf, transfer);
            }

            return xml;
        }

        /// <summary>
        /// Generate the Transaction XML part
        /// </summary>
        /// <param name="pmtInf">The root nodes for a transaction</param>
        /// <param name="transfer">The transaction to generate</param>
        private void GenerateTransaction(XmlElement pmtInf, SepaCreditTransferTransaction transfer)
        {
            var cdtTrfTxInf = pmtInf.NewElement("CdtTrfTxInf");
            var pmtId = cdtTrfTxInf.NewElement("PmtId");
            if (transfer.Id != null)
                pmtId.NewElement("InstrId", transfer.Id);
            pmtId.NewElement("EndToEndId", transfer.EndToEndId);
            cdtTrfTxInf.NewElement("Amt")
                       .NewElement("InstdAmt", StringUtils.FormatAmount(transfer.Amount))
                       .SetAttribute("Ccy", transfer.Currency);
            XmlUtils.CreateBic(cdtTrfTxInf.NewElement("CdtrAgt"), transfer.Creditor);
            var cdtr = cdtTrfTxInf.NewElement("Cdtr");
            cdtr.NewElement("Nm", transfer.Creditor.Name);
            if (transfer.Creditor.Address != null)
            {
                AddPostalAddressElements(cdtr, transfer.Creditor.Address);
            }

            cdtTrfTxInf.NewElement("CdtrAcct").NewElement("Id").NewElement("IBAN", transfer.Creditor.Iban);

            if (IsInternational && transfer.SepaInstructionForCreditor != null)
            {
                var instr = cdtTrfTxInf.NewElement("InstrForCdtrAgt");
                instr.NewElement("Cd", transfer.SepaInstructionForCreditor.Code);
                if (!string.IsNullOrEmpty(transfer.SepaInstructionForCreditor.Comment))
                {
                    instr.NewElement("InstrInf", transfer.SepaInstructionForCreditor.Comment);
                }
            }

            if (!string.IsNullOrEmpty(transfer.Purpose)) {
				cdtTrfTxInf.NewElement("Purp").NewElement("Cd", transfer.Purpose);
			}

            if (IsInternational && !string.IsNullOrEmpty(transfer.RegulatoryReportingCode))
            {
                cdtTrfTxInf.NewElement("RgltryRptg").NewElement("Dtls").NewElement("Cd", transfer.RegulatoryReportingCode);
            }

            if (!string.IsNullOrEmpty(transfer.RemittanceInformation)) {
				cdtTrfTxInf.NewElement("RmtInf").NewElement("Ustrd", transfer.RemittanceInformation);
			}
        }
        protected override bool CheckSchema(SepaSchema aSchema)
        {
            return aSchema == SepaSchema.Pain00100103 || aSchema == SepaSchema.Pain00100104;
        }
    }
}
