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
        ///     Debtor IBAN data for ultimate debtor
        /// </summary>
        /// <exception cref="SepaRuleException">If debtor to set is not valid.</exception>
        public SepaIbanData UltimateDebtor
        {
            get { return UltimateSepaIban; }
            set
            {
                //if (!value.IsValid || value.UnknownBic)
                //    throw new SepaRuleException("Debtor IBAN data are invalid.");
                UltimateSepaIban = value;
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
        public void AddCreditTransfer(SepaCreditTransferTransaction? transfer)
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
            var cstmrCdtTrfInitn = xml.SelectSingleNode("//CstmrCdtTrfInitn");
            if (cstmrCdtTrfInitn == null)
                throw new Exception("CstmrCdtTrfInitn is Null");
            var grpHdr = cstmrCdtTrfInitn.SelectSingleNode("GrpHdr");
            if (grpHdr == null)
                throw new Exception("GrpHdr is Null");

            if(grpHdr.SelectSingleNode("MsgId") != null)
                MessageIdentification = grpHdr.SelectSingleNode("MsgId").InnerText;

            if(grpHdr.SelectSingleNode("CreDtTm") != null) 
                CreationDate = Convert.ToDateTime(grpHdr.SelectSingleNode("CreDtTm").InnerText);

            //if(grpHdr.SelectSingleNode("NbOfTxs") != null)
            //    numberOfTransactions = Convert.ToInt32(grpHdr.SelectSingleNode("NbOfTxs").InnerText);

            //if(grpHdr.SelectSingleNode("CtrlSum") != null)
            //    headerControlSum = Convert.ToDecimal(grpHdr.SelectSingleNode("CtrlSum").InnerText.Replace(".",","));

            var initgPty = grpHdr.SelectSingleNode("InitgPty");
            if (initgPty == null)
                throw new Exception("InitgPty is Null");

            if(initgPty.SelectSingleNode("Nm") != null)
                InitiatingPartyName = initgPty.SelectSingleNode("Nm").InnerText;

            if(initgPty.SelectSingleNode("Id/OrgId/Othr/Id") != null)
                InitiatingPartyId = initgPty.SelectSingleNode("Id/OrgId/Othr/Id").InnerText;

            // Part 2: Payment Information
            var pmtInf = xml.SelectSingleNode("//CstmrCdtTrfInitn/PmtInf");
            if (pmtInf == null)
                throw new Exception("CstmrCdtTrfInitn/PmtInf is Null");

            if(pmtInf.SelectSingleNode("PmtInfId") != null)
                PaymentInfoId = pmtInf.SelectSingleNode("PmtInfId").InnerText;

            if (PaymentInfoId == null)
                PaymentInfoId = MessageIdentification;

            //Constant.CreditTransfertPaymentMethod = XmlUtils.SelectSingleNode(pmtInf, "PmtMtd").InnerText;

            //if(pmtInf.SelectSingleNode("NbOfTxs") != null)
            //    numberOfTransactions = Convert.ToInt32(pmtInf.SelectSingleNode("NbOfTxs").InnerText);

            //if(pmtInf.SelectSingleNode("CtrlSum") != null)
            //    paymentControlSum = Convert.ToDecimal(pmtInf.SelectSingleNode("CtrlSum").InnerText);

            var pmtTpInf = pmtInf.SelectSingleNode("PmtTpInf");
            if (pmtTpInf != null)
            {

                if (pmtTpInf.SelectSingleNode("InstrPrty") != null && pmtTpInf.SelectSingleNode("InstrPrty").InnerText == "NORM")
                    IsInternational = true;

                if (pmtTpInf.SelectSingleNode("SvcLvl/Cd") != null && pmtTpInf.SelectSingleNode("SvcLvl/Cd").InnerText == "SEPA")
                    IsInternational = false;

                if (pmtTpInf.SelectSingleNode("LclInstr/Cd") != null)
                    LocalInstrumentCode = pmtTpInf.SelectSingleNode("LclInstr/Cd").InnerText;

                if (pmtTpInf.SelectSingleNode("CtgyPurp/Cd") != null)
                    CategoryPurposeCode = pmtTpInf.SelectSingleNode("CtgyPurp/Cd").InnerText;
            }

            if (pmtInf.SelectSingleNode("ReqdExctnDt") != null)
                RequestedExecutionDate = Convert.ToDateTime(pmtInf.SelectSingleNode("ReqdExctnDt").InnerText);

            var dbtr = pmtInf.SelectSingleNode("Dbtr");
            if (dbtr == null)
                throw new Exception("Dbtr is Null");

            Debtor = new SepaIbanData();
            Debtor.Address = new SepaPostalAddress();

            if(dbtr.SelectSingleNode("Nm") != null)
                Debtor.Name = dbtr.SelectSingleNode("Nm").InnerText;

            ReadSepaPostalAddress(dbtr, Debtor.Address);


            var ultimatedbtr = pmtInf.SelectSingleNode("UltmtDbtr");
            if (ultimatedbtr == null)
                throw new Exception("UltmtDbtr is Null");

            UltimateDebtor = new SepaIbanData();
            UltimateDebtor.Address = new SepaPostalAddress();

            if (ultimatedbtr.SelectSingleNode("Nm") != null)
                UltimateDebtor.Name = ultimatedbtr.SelectSingleNode("Nm").InnerText;

            ReadSepaPostalAddress(ultimatedbtr, UltimateDebtor.Address);


            if (pmtInf.SelectSingleNode("Dbtr/Id/OrgId/Othr/Id") != null)
                InitiatingPartyId = pmtInf.SelectSingleNode("Dbtr/Id/OrgId/Othr/Id").InnerText;

            if (pmtInf.SelectSingleNode("DbtrAcct/Id/IBAN") != null)
                Debtor.Iban = pmtInf.SelectSingleNode("DbtrAcct/Id/IBAN").InnerText;

            if (pmtInf.SelectSingleNode("DbtrAcct/Id/IBAN/Ccy") != null)
                DebtorAccountCurrency = pmtInf.SelectSingleNode("DbtrAcct/Id/IBAN/Ccy").InnerText;

            var finInstnId = pmtInf.SelectSingleNode("DbtrAgt/FinInstnId");
            if (finInstnId == null)
                throw new Exception("DbtrAgt/FinInstnId is Null");

            if(finInstnId.SelectSingleNode("BIC") != null)
                Debtor.Bic = finInstnId.SelectSingleNode("BIC").InnerText;

            Debtor.AgentAddress = new SepaPostalAddress();
            ReadSepaPostalAddress(finInstnId, Debtor.AgentAddress);

            if (IsInternational)
            {
                if (pmtInf.SelectSingleNode("ChrgBr") != null)
                {
                    Enum.TryParse(pmtInf.SelectSingleNode("ChrgBr").InnerText, out SepaChargeBearer chargeBearer);
                    ChargeBearer = chargeBearer;
                }
            }

            // Part 3: Credit Transfer Transaction Information
            var cdtTrfTxInf = pmtInf.SelectNodes("CdtTrfTxInf");
            if (cdtTrfTxInf != null)
            {
                foreach (XmlNode node in cdtTrfTxInf)
                {
                    SepaCreditTransferTransaction transfer = new SepaCreditTransferTransaction();

                    if (node.SelectSingleNode("PmtId/InstrId") != null)
                        transfer.Id = node.SelectSingleNode("PmtId/InstrId").InnerText;

                    if (node.SelectSingleNode("PmtId/EndToEndId") != null)
                        transfer.EndToEndId = node.SelectSingleNode("PmtId/EndToEndId").InnerText;

                    if(node.SelectSingleNode("Amt/InstdAmt") != null)
                        transfer.Amount = Convert.ToDecimal(node.SelectSingleNode("Amt/InstdAmt").InnerText.Replace(".", ","));

                    if(node.SelectSingleNode("Amt/InstdAmt") != null)
                        if(node.SelectSingleNode("Amt/InstdAmt").Attributes["Ccy"] != null)
                            transfer.Currency = node.SelectSingleNode("Amt/InstdAmt").Attributes["Ccy"].Value;

                    transfer.Creditor = new SepaIbanData();
                    var cdtrAgt = node.SelectSingleNode("CdtrAgt");
                    if (cdtrAgt == null)
                        throw new Exception("CdtrAgt is Null");

                    if(cdtrAgt.SelectSingleNode("FinInstnId/BIC") != null)
                        transfer.Creditor.Bic = cdtrAgt.SelectSingleNode("FinInstnId/BIC").InnerText;
                    
                    var cdtr = node.SelectSingleNode("Cdtr");
                    if (cdtr == null)
                        throw new Exception("Cdtr is Null");

                    if(cdtr.SelectSingleNode("Nm") != null)
                        transfer.Creditor.Name = cdtr.SelectSingleNode("Nm").InnerText;

                    transfer.Creditor.Address = new SepaPostalAddress();
                    ReadSepaPostalAddress(cdtr, transfer.Creditor.Address);

                    var cdtrAcct = node.SelectSingleNode("CdtrAcct");
                    if (cdtrAcct == null)
                        throw new Exception("CdtrAcct is Null");

                    if(cdtrAcct.SelectSingleNode("Id/IBAN") != null)
                        transfer.Creditor.Iban = cdtrAcct.SelectSingleNode("Id/IBAN").InnerText;

                    if (IsInternational)
                    {
                        var instr = node.SelectSingleNode("InstrForCdtrAgt");
                        if (instr != null)
                        {
                            if (instr.SelectSingleNode("Cd") != null)
                            {
                                Enum.TryParse(instr.SelectSingleNode("Cd").InnerText, out SepaInstructionForCreditorCode code);
                                transfer.SepaInstructionForCreditor.Code = code;
                            }

                            if(instr.SelectSingleNode("InstrInf") != null)
                                transfer.SepaInstructionForCreditor.Comment = instr.SelectSingleNode("InstrInf").InnerText;
                        }
                    }

                    if(node.SelectSingleNode("Purp/Cd") != null)
                        transfer.Purpose = node.SelectSingleNode("Purp/Cd").InnerText;
                    
                    if (IsInternational)
                    {
                        if(node.SelectSingleNode("RgltryRptg/Dtls/Cd") != null)
                            transfer.RegulatoryReportingCode = node.SelectSingleNode("RgltryRptg/Dtls/Cd").InnerText;
                    }

                    if(node.SelectSingleNode("RmtInf/Ustrd") != null)
                        transfer.RemittanceInformation = node.SelectSingleNode("RmtInf/Ustrd").InnerText;

                    AddCreditTransfer(transfer);
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
            var grpHdr = (el.SelectSingleNode("CstmrCdtTrfInitn") as XmlElement).NewElement("GrpHdr");
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
            var pmtInf = (el.SelectSingleNode("CstmrCdtTrfInitn") as XmlElement).NewElement("PmtInf");
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
                (pmtInf.SelectSingleNode("PmtTpInf") as XmlElement).NewElement("LclInstr")
                        .NewElement("Cd", LocalInstrumentCode);

			if (CategoryPurposeCode != null) {
                (pmtInf.SelectSingleNode("PmtTpInf") as XmlElement).
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
            
            if (InitiatingPartyId != null)
            {
                dbtr.NewElement("Id").NewElement("OrgId").
                    NewElement("Othr").NewElement("Id", InitiatingPartyId);
            }

            var ultimatedbtr = pmtInf.NewElement("UltmtDbtr");
            ultimatedbtr.NewElement("Nm", UltimateDebtor.Name);
            if (UltimateDebtor.Address != null)
            {
                AddPostalAddressElements(ultimatedbtr, UltimateDebtor.Address);
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
