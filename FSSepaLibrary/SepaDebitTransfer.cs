using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using FSException;
using FSSepaLibrary.Utils;

namespace FSSepaLibrary
{
    /// <summary>
    ///     Manage SEPA (Single Euro Payments Area) CreditTransfer for SEPA or international order.
    ///     Only one PaymentInformation is managed but it can manage multiple transactions.
    /// </summary>
    public class SepaDebitTransfer : SepaTransfer<SepaDebitTransferTransaction>
    {
        /// <summary>
        ///     creditor account ISO currency code (default is EUR)
        /// </summary>
        public string CreditorAccountCurrency { get; set; }

        /// <summary>
        ///     Unique and unambiguous identification of a person. SEPA creditor
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// Create a Sepa Debit Transfer using Pain.008.001.02 schema
        /// </summary>
        public SepaDebitTransfer()
        {
            CreditorAccountCurrency = Constant.EuroCurrency;
            LocalInstrumentCode = "CORE";
            schema = SepaSchema.Pain00800102;
        }

        /// <summary>
        /// Create a Sepa Debit Transfer using Pain.008.001.02 schema
        /// </summary>
        public SepaDebitTransfer(string xmlFile)
        {
            CreditorAccountCurrency = Constant.EuroCurrency;
            LocalInstrumentCode = "CORE";
            schema = SepaSchema.Pain00800102;

            LoadXml(xmlFile);
        }

        /// <summary>
        ///     Creditor IBAN data
        /// </summary>
        /// <exception cref="SepaRuleException">If creditor to set is not valid.</exception>
        public SepaIbanData Creditor
        {
            get { return SepaIban; }
            set
            {
                //if (!value.IsValid || value.UnknownBic)
                //    throw new SepaRuleException("Creditor IBAN data are invalid.");
                SepaIban = value;
            }
        }

        /// <summary>
        ///     Creditor IBAN data for ultimate creditor
        /// </summary>
        /// <exception cref="SepaRuleException">If creditor to set is not valid.</exception>
        public SepaIbanData UltimateCreditor
        {
            get { return UltimateSepaIban; }
            set
            {
                //if (!value.IsValid || value.UnknownBic)
                //    throw new SepaRuleException("Creditor IBAN data are invalid.");
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

            if (Creditor == null)
            {
                throw new SepaRuleException("The creditor is mandatory.");
            }
        }

        /// <summary>
        ///     Add an existing transfer transaction
        /// </summary>
        /// <param name="transfer"></param>
        /// <exception cref="ArgumentNullException">If transfert is null.</exception>
        public void AddDebitTransfer(SepaDebitTransferTransaction transfer)
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
            var cstmrDrctDbtInitn = xml.SelectSingleNode("//CstmrDrctDbtInitn");
            if (cstmrDrctDbtInitn == null)
                throw new ExceptionUtil("CstmrDrctDbtInitn is Null");
            var grpHdr = cstmrDrctDbtInitn.SelectSingleNode("GrpHdr");
            if (grpHdr == null)
                throw new ExceptionUtil("GrpHdr is Null");

            if (grpHdr.SelectSingleNode("MsgId") != null)
                MessageIdentification = grpHdr.SelectSingleNode("MsgId").InnerText;

            if (grpHdr.SelectSingleNode("CreDtTm") != null)
                CreationDate = Convert.ToDateTime(grpHdr.SelectSingleNode("CreDtTm").InnerText);

            //if (grpHdr.SelectSingleNode("NbOfTxs") != null)
            //    numberOfTransactions = Convert.ToInt32(grpHdr.SelectSingleNode("NbOfTxs").InnerText);

            //if (grpHdr.SelectSingleNode("CtrlSum") != null)
            //    headerControlSum = Convert.ToDecimal(grpHdr.SelectSingleNode("CtrlSum").InnerText.Replace(".", ","));

            var initgPty = grpHdr.SelectSingleNode("InitgPty");
            if (initgPty == null)
                throw new ExceptionUtil("InitgPty is Null");

            if (initgPty.SelectSingleNode("Nm") != null)
                InitiatingPartyName = initgPty.SelectSingleNode("Nm").InnerText;

            if (initgPty.SelectSingleNode("Id/OrgId/Othr/Id") != null)
                InitiatingPartyId = initgPty.SelectSingleNode("Id/OrgId/Othr/Id").InnerText;

            // Part 2: Payment Information
            var pmtInf = xml.SelectSingleNode("//CstmrDrctDbtInitn/PmtInf");
            if (pmtInf == null)
                throw new ExceptionUtil("CstmrDrctDbtInitn/PmtInf is Null");

            if (pmtInf.SelectSingleNode("PmtInfId") != null)
                PaymentInfoId = pmtInf.SelectSingleNode("PmtInfId").InnerText;

            if (PaymentInfoId == null)
                PaymentInfoId = MessageIdentification;

            if(pmtInf.SelectSingleNode("CtgyPurp/Cd"    ) != null)
                CategoryPurposeCode = pmtInf.SelectSingleNode("CtgyPurp/Cd").InnerText;

            //if (XmlUtils.SelectSingleNode(pmtInf, "PmtMtd") != null)
            //    Constant.DebitTransfertPaymentMethod = XmlUtils.SelectSingleNode(pmtInf, "PmtMtd").InnerText;

            //if (pmtInf.SelectSingleNode("NbOfTxs") != null)
            //    numberOfTransactions = Convert.ToInt32(pmtInf.SelectSingleNode("NbOfTxs").InnerText);

            //if (pmtInf.SelectSingleNode("CtrlSum") != null)
            //    paymentControlSum = Convert.ToDecimal(pmtInf.SelectSingleNode("CtrlSum").InnerText);

            if(pmtInf.SelectSingleNode("CdtrSchmeId/Id/PrvtId/Othr/Id") != null)
                PersonId = pmtInf.SelectSingleNode("CdtrSchmeId/Id/PrvtId/Othr/Id").InnerText;

            var pmtTpInf = pmtInf.SelectSingleNode("PmtTpInf");
            if (pmtTpInf != null)
            {
                if (pmtTpInf.SelectSingleNode("LclInstr/Cd") != null)
                    LocalInstrumentCode = pmtTpInf.SelectSingleNode("LclInstr/Cd").InnerText;

                if (pmtTpInf.SelectSingleNode("CtgyPurp/Cd") != null)
                    CategoryPurposeCode = pmtTpInf.SelectSingleNode("CtgyPurp/Cd").InnerText;
            }

            if (pmtInf.SelectSingleNode("ReqdExctnDt") != null)
                RequestedExecutionDate = Convert.ToDateTime(pmtInf.SelectSingleNode("ReqdExctnDt").InnerText);

            var cdtr = pmtInf.SelectSingleNode("Cdtr");
            if (cdtr == null)
                throw new ExceptionUtil("Cdtr is Null");

            Creditor = new SepaIbanData();
            Creditor.Address = new SepaPostalAddress();

            if (cdtr.SelectSingleNode("Nm") != null)
                Creditor.Name = cdtr.SelectSingleNode("Nm").InnerText;

            ReadSepaPostalAddress(cdtr, Creditor.Address);


            var ultimatecdtr = pmtInf.SelectSingleNode("UltimateCdtr");
            if (ultimatecdtr == null)
                throw new ExceptionUtil("UltimateCdtr is Null");

            UltimateCreditor = new SepaIbanData();
            UltimateCreditor.Address = new SepaPostalAddress();

            if (ultimatecdtr.SelectSingleNode("Nm") != null)
                UltimateCreditor.Name = ultimatecdtr.SelectSingleNode("Nm").InnerText;

            ReadSepaPostalAddress(ultimatecdtr, UltimateCreditor.Address);


            if (pmtInf.SelectSingleNode("Cdtr/Id/OrgId/Othr/Id") != null)
                InitiatingPartyId = pmtInf.SelectSingleNode("Cdtr/Id/OrgId/Othr/Id").InnerText;

            if (pmtInf.SelectSingleNode("CdtrAcct/Id/IBAN") != null)
                Creditor.Iban = pmtInf.SelectSingleNode("CdtrAcct/Id/IBAN").InnerText;

            if (pmtInf.SelectSingleNode("CdtrAcct/Id/IBAN/Ccy") != null)
                CreditorAccountCurrency = pmtInf.SelectSingleNode("CdtrAcct/Id/IBAN/Ccy").InnerText;

            var finInstnId = pmtInf.SelectSingleNode("CdtrAgt/FinInstnId");
            if (finInstnId == null)
                throw new ExceptionUtil("CdtrAgt/FinInstnId is Null");

            if (finInstnId.SelectSingleNode("BIC") != null)
                Creditor.Bic = finInstnId.SelectSingleNode("BIC").InnerText;

            Creditor.AgentAddress = new SepaPostalAddress();
            ReadSepaPostalAddress(finInstnId, Creditor.AgentAddress);

            // Part 3: Credit Transfer Transaction Information
            var drctDbtTxInf = pmtInf.SelectNodes("DrctDbtTxInf");
            if (drctDbtTxInf != null)
            {
                foreach (XmlNode node in drctDbtTxInf)
                {
                    SepaDebitTransferTransaction transfer = new SepaDebitTransferTransaction();

                    if (node.SelectSingleNode("PmtId/InstrId") != null)
                        transfer.Id = node.SelectSingleNode("PmtId/InstrId").InnerText;

                    if (node.SelectSingleNode("EndToEndId") != null)
                        transfer.EndToEndId = node.SelectSingleNode("EndToEndId").InnerText;

                    if (node.SelectSingleNode("Amt/InstdAmt") != null)
                        transfer.Amount = Convert.ToDecimal(node.SelectSingleNode("Amt/InstdAmt").InnerText.Replace(".", ","));

                    if (node.SelectSingleNode("Amt/InstdAmt") != null)
                        if(node.SelectSingleNode("Amt/InstdAmt").Attributes["Ccy"] != null)
                            transfer.Currency = node.SelectSingleNode("Amt/InstdAmt").Attributes["Ccy"].Value;

                    transfer.Debtor = new SepaIbanData();
                    var cdtrAgt = node.SelectSingleNode("//CdtrAgt");
                    if (cdtrAgt == null)
                        throw new ExceptionUtil("CdtrAgt is Null");

                    if (cdtrAgt.SelectSingleNode("FinInstnId/BIC") != null)
                        transfer.Debtor.Bic = cdtrAgt.SelectSingleNode("FinInstnId/BIC").InnerText;

                    var dbtr = node.SelectSingleNode("Dbtr");
                    if (dbtr == null)
                        throw new ExceptionUtil("Dbtr is Null");

                    if (dbtr.SelectSingleNode("Nm") != null)
                        transfer.Debtor.Name = dbtr.SelectSingleNode("Nm").InnerText;

                    transfer.Debtor.Address = new SepaPostalAddress();
                    ReadSepaPostalAddress(dbtr, transfer.Debtor.Address);

                    var dbtrAcct = node.SelectSingleNode("DbtrAcct");
                    if (dbtrAcct == null)
                        throw new ExceptionUtil("DbtrAcct is Null");

                    if (dbtrAcct.SelectSingleNode("Id/IBAN") != null)
                        transfer.Debtor.Iban = dbtrAcct.SelectSingleNode("Id/IBAN").InnerText;

                    if (node.SelectSingleNode("Purp/Cd") != null)
                        transfer.Purpose = node.SelectSingleNode("Purp/Cd").InnerText;

                    if (node.SelectSingleNode("RmtInf/Ustrd") != null)
                        transfer.RemittanceInformation = node.SelectSingleNode("RmtInf/Ustrd").InnerText;

                    AddDebitTransfer(transfer);
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

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            XmlElement el = (XmlElement)xml.AppendChild(xml.CreateElement("Document"));
            if (el != null)
            {
                el.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                el.SetAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:" + SepaSchemaUtils.SepaSchemaToString(schema));
                el.NewElement("CstmrDrctDbtInitn");

                // Part 1: Group Header
                XmlElement grpHdr = (el.SelectSingleNode("CstmrDrctDbtInitn") as XmlElement).NewElement("GrpHdr");
                grpHdr.NewElement("MsgId", MessageIdentification);
                grpHdr.NewElement("CreDtTm", StringUtils.FormatDateTime(CreationDate));
                grpHdr.NewElement("NbOfTxs", numberOfTransactions);
                grpHdr.NewElement("CtrlSum", StringUtils.FormatAmount(headerControlSum));
                grpHdr.NewElement("InitgPty").NewElement("Nm", InitiatingPartyName);
                if (InitiatingPartyId != null)
                {
                    (grpHdr.SelectSingleNode("InitgPty") as XmlElement).
                        NewElement("Id").NewElement("OrgId").
                        NewElement("Othr").NewElement("Id", InitiatingPartyId);
                }
            }

            // Part 2: Payment Information for each Sequence Type.
            foreach (SepaSequenceType seqTp in Enum.GetValues(typeof(SepaSequenceType)))
            {
                var seqTransactions = transactions.FindAll(d => d.SequenceType == seqTp);
                var pmtInf = GeneratePaymentInformation(xml, seqTp, seqTransactions);
                // If a payment information has been created
                if (pmtInf != null)
                {
                    // Part 3: Debit Transfer Transaction Information
                    foreach (var transfer in seqTransactions)
                    {
                        GenerateTransaction(pmtInf, transfer);
                    }
                }
            }

            return xml;
        }

        /// <summary>
        /// Generate a Payment Information node for a Sequence Type.
        /// </summary>
        /// <param name="xml">The XML object to write</param>
        /// <param name="sqType">The Sequence Type</param>
        /// <param name="seqTransactions">The transactions of the specified type</param>
        private XmlElement GeneratePaymentInformation(XmlDocument xml, SepaSequenceType sqType, IEnumerable<SepaDebitTransferTransaction> seqTransactions)
        {
            int controlNumber = 0;
            decimal controlSum = 0;

            // We check the number of transaction to write and the sum due to the Sequence Type.
            foreach (var transfer in seqTransactions)
            {
                controlNumber += 1;
                controlSum += transfer.Amount;
            }

            // If there is no transaction, we end the method here.
            if (controlNumber == 0)
                return null;

            var pmtInf = (xml.SelectSingleNode("//CstmrDrctDbtInitn") as XmlElement).NewElement("PmtInf");
            pmtInf.NewElement("PmtInfId", PaymentInfoId ?? MessageIdentification);
            if (CategoryPurposeCode != null)
                pmtInf.NewElement("CtgyPurp").NewElement("Cd", CategoryPurposeCode);

            pmtInf.NewElement("PmtMtd", Constant.DebitTransfertPaymentMethod);
            pmtInf.NewElement("NbOfTxs", controlNumber);
            pmtInf.NewElement("CtrlSum", StringUtils.FormatAmount(controlSum));

            var pmtTpInf = pmtInf.NewElement("PmtTpInf");
            pmtTpInf.NewElement("SvcLvl").NewElement("Cd", "SEPA");
            pmtTpInf.NewElement("LclInstrm").NewElement("Cd", LocalInstrumentCode);
            pmtTpInf.NewElement("SeqTp", SepaSequenceTypeUtils.SepaSequenceTypeToString(sqType));

            pmtInf.NewElement("ReqdColltnDt", StringUtils.FormatDate(RequestedExecutionDate));
            pmtInf.NewElement("Cdtr").NewElement("Nm", Creditor.Name);
            pmtInf.NewElement("UltmtCdtr").NewElement("Nm", UltimateCreditor.Name);

            var dbtrAcct = pmtInf.NewElement("CdtrAcct");
            dbtrAcct.NewElement("Id").NewElement("IBAN", Creditor.Iban);
            dbtrAcct.NewElement("Ccy", CreditorAccountCurrency);

            pmtInf.NewElement("CdtrAgt").NewElement("FinInstnId").NewElement("BIC", Creditor.Bic);
            pmtInf.NewElement("ChrgBr", "SLEV");

            var othr = pmtInf.NewElement("CdtrSchmeId").NewElement("Id")
                    .NewElement("PrvtId")
                        .NewElement("Othr");
            othr.NewElement("Id", PersonId);
            othr.NewElement("SchmeNm").NewElement("Prtry", "SEPA");

            return pmtInf;
        }

        /// <summary>
        /// Generate the Transaction XML part
        /// </summary>
        /// <param name="pmtInf">The root nodes for a transaction</param>
        /// <param name="transfer">The transaction to generate</param>
        private static void GenerateTransaction(XmlElement pmtInf, SepaDebitTransferTransaction transfer)
        {
            var cdtTrfTxInf = pmtInf.NewElement("DrctDbtTxInf");
            var pmtId = cdtTrfTxInf.NewElement("PmtId");
            if (transfer.Id != null)
                pmtId.NewElement("InstrId", transfer.Id);
            pmtId.NewElement("EndToEndId", transfer.EndToEndId);
            cdtTrfTxInf.NewElement("InstdAmt", StringUtils.FormatAmount(transfer.Amount)).SetAttribute("Ccy", transfer.Currency);

            var mndtRltdInf = cdtTrfTxInf.NewElement("DrctDbtTx").NewElement("MndtRltdInf");
            mndtRltdInf.NewElement("MndtId", transfer.MandateIdentification);
            mndtRltdInf.NewElement("DtOfSgntr", StringUtils.FormatDate(transfer.DateOfSignature));

            XmlUtils.CreateBic(cdtTrfTxInf.NewElement("DbtrAgt"), transfer.Debtor);
            cdtTrfTxInf.NewElement("Dbtr").NewElement("Nm", transfer.Debtor.Name);
            cdtTrfTxInf.NewElement("DbtrAcct").NewElement("Id").NewElement("IBAN", transfer.Debtor.Iban);

            if (!string.IsNullOrEmpty(transfer.RemittanceInformation))
                cdtTrfTxInf.NewElement("RmtInf").NewElement("Ustrd", transfer.RemittanceInformation);
        }

        protected override bool CheckSchema(SepaSchema aSchema)
        {
            return aSchema == SepaSchema.Pain00800102 || aSchema == SepaSchema.Pain00800103;
        }
    }
}
