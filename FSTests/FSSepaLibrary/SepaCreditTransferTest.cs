﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using FSSepaLibrary.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibrary.Test
{
    [TestClass]
    public class SepaCreditTransferTest
    {
        private static readonly SepaIbanData Debtor = new SepaIbanData
            {
                Bic = "SOGEFRPPXXX",
                Iban = "FR7030002005500000157845Z02",
                Name = "My Corp"
            };

        private readonly string FILENAME = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/sepa_credit_test_result.xml";

        private const string ONE_ROW_RESULT =
            "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"urn:iso:std:iso:20022:tech:xsd:pain.001.001.03\"><CstmrCdtTrfInitn><GrpHdr><MsgId>transferID</MsgId><CreDtTm>2013-02-17T22:38:12</CreDtTm><NbOfTxs>1</NbOfTxs><CtrlSum>23.45</CtrlSum><InitgPty><Nm>Me</Nm><Id><OrgId><Othr><Id>MyId</Id></Othr></OrgId></Id></InitgPty></GrpHdr><PmtInf><PmtInfId>paymentInfo</PmtInfId><PmtMtd>TRF</PmtMtd><NbOfTxs>1</NbOfTxs><CtrlSum>23.45</CtrlSum><PmtTpInf><SvcLvl><Cd>SEPA</Cd></SvcLvl><LclInstr><Cd>MyCode</Cd></LclInstr></PmtTpInf><ReqdExctnDt>2013-02-17</ReqdExctnDt><Dbtr><Nm>My Corp</Nm><Id><OrgId><Othr><Id>MyId</Id></Othr></OrgId></Id></Dbtr><DbtrAcct><Id><IBAN>FR7030002005500000157845Z02</IBAN></Id><Ccy>EUR</Ccy></DbtrAcct><DbtrAgt><FinInstnId><BIC>SOGEFRPPXXX</BIC></FinInstnId></DbtrAgt><ChrgBr>SLEV</ChrgBr><CdtTrfTxInf><PmtId><InstrId>Transaction Id 1</InstrId><EndToEndId>paymentInfo/1</EndToEndId></PmtId><Amt><InstdAmt Ccy=\"EUR\">23.45</InstdAmt></Amt><CdtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></CdtrAgt><Cdtr><Nm>THEIR_NAME</Nm></Cdtr><CdtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></CdtrAcct><RmtInf><Ustrd>Transaction description</Ustrd></RmtInf></CdtTrfTxInf></PmtInf></CstmrCdtTrfInitn></Document>";

        private const string MULTIPLE_ROW_RESULT =
            "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"urn:iso:std:iso:20022:tech:xsd:pain.001.001.03\"><CstmrCdtTrfInitn><GrpHdr><MsgId>transferID</MsgId><CreDtTm>2013-02-17T22:38:12</CreDtTm><NbOfTxs>3</NbOfTxs><CtrlSum>63.36</CtrlSum><InitgPty><Nm>Me</Nm></InitgPty></GrpHdr><PmtInf><PmtInfId>paymentInfo</PmtInfId><PmtMtd>TRF</PmtMtd><NbOfTxs>3</NbOfTxs><CtrlSum>63.36</CtrlSum><PmtTpInf><SvcLvl><Cd>SEPA</Cd></SvcLvl></PmtTpInf><ReqdExctnDt>2013-02-18</ReqdExctnDt><Dbtr><Nm>My Corp</Nm><PstlAdr><AdrLine>address</AdrLine></PstlAdr></Dbtr><DbtrAcct><Id><IBAN>FR7030002005500000157845Z02</IBAN></Id><Ccy>EUR</Ccy></DbtrAcct><DbtrAgt><FinInstnId><BIC>SOGEFRPPXXX</BIC></FinInstnId></DbtrAgt><ChrgBr>SLEV</ChrgBr><CdtTrfTxInf><PmtId><InstrId>Transaction Id 1</InstrId><EndToEndId>multiple1</EndToEndId></PmtId><Amt><InstdAmt Ccy=\"EUR\">23.45</InstdAmt></Amt><CdtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></CdtrAgt><Cdtr><Nm>THEIR_NAME</Nm><PstlAdr><AdrLine>add1</AdrLine><AdrLine>add2</AdrLine></PstlAdr></Cdtr><CdtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></CdtrAcct><RmtInf><Ustrd>Transaction description</Ustrd></RmtInf></CdtTrfTxInf><CdtTrfTxInf><PmtId><InstrId>Transaction Id 2</InstrId><EndToEndId>paymentInfo/2</EndToEndId></PmtId><Amt><InstdAmt Ccy=\"EUR\">12.56</InstdAmt></Amt><CdtrAgt><FinInstnId><BIC>AGRIFRPPXXX</BIC></FinInstnId></CdtrAgt><Cdtr><Nm>THEIR_SECOND_NAME</Nm><PstlAdr><AdrLine>add3</AdrLine><AdrLine>add4</AdrLine></PstlAdr></Cdtr><CdtrAcct><Id><IBAN>FR1420041010050500013M02606</IBAN></Id></CdtrAcct><RmtInf><Ustrd>Transaction description 2</Ustrd></RmtInf></CdtTrfTxInf><CdtTrfTxInf><PmtId><InstrId>Transaction Id 3</InstrId><EndToEndId>paymentInfo/3</EndToEndId></PmtId><Amt><InstdAmt Ccy=\"EUR\">27.35</InstdAmt></Amt><CdtrAgt><FinInstnId><BIC>BANK_BIC</BIC></FinInstnId></CdtrAgt><Cdtr><Nm>NAME</Nm></Cdtr><CdtrAcct><Id><IBAN>ACCOUNT_IBAN_SAMPLE</IBAN></Id></CdtrAcct><RmtInf><Ustrd>Transaction description 3</Ustrd></RmtInf></CdtTrfTxInf></PmtInf></CstmrCdtTrfInitn></Document>";

        private static SepaCreditTransferTransaction CreateTransaction(string id, decimal amount, string information)
        {
            return new SepaCreditTransferTransaction
            {
                Id = id,
                Creditor = new SepaIbanData
                {
                    Bic = "AGRIFRPPXXX",
                    Iban = "FR1420041010050500013M02606",
                    Name = "THEIR_NAME"
                },
                Amount = amount,
                RemittanceInformation = information
            };
        }

        private static SepaCreditTransfer GetEmptyCreditTransfert()
        {
            return new SepaCreditTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 17),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Debtor = Debtor
            };
        }

        private static SepaCreditTransfer GetOneTransactionCreditTransfert(decimal amount)
        {
            var transfert = GetEmptyCreditTransfert();
            transfert.InitiatingPartyId = "MyId";
            transfert.LocalInstrumentCode = "MyCode";

            transfert.AddCreditTransfer(CreateTransaction("Transaction Id 1", amount, "Transaction description"));
            return transfert;
        }

        //[OneTimeTearDown]
        //public void Cleanup()
        //{
        //    if (File.Exists(FILENAME))
        //        File.Delete(FILENAME);
        //}

        [TestMethod]
        public void ShouldAllowMultipleNullIdTransations()
        {
            const decimal amount = 23.45m;

            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(amount);

            transfert.AddCreditTransfer(CreateTransaction(null, amount, "Transaction description 1"));
            transfert.AddCreditTransfer(CreateTransaction(null, amount, "Transaction description 2"));
        }

        [TestMethod]
        public void ShouldAllowTransactionWithoutRemittanceInformation()
        {
            var transfert = GetEmptyCreditTransfert();
            transfert.AddCreditTransfer(CreateTransaction(null, 12m, null));
            transfert.AddCreditTransfer(CreateTransaction(null, 13m, null));

            string result = transfert.AsXmlString();
            Assert.IsFalse(result.Contains("<RmtInf>"));
        }

        [TestMethod]
        public void ShouldKeepEndToEndIdIfSet()
        {
            const decimal amount = 23.45m;

            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(amount);

            var trans = CreateTransaction(null, amount, "Transaction description 2");
            trans.EndToEndId = "endToendId1";
            transfert.AddCreditTransfer(trans);

            trans = CreateTransaction(null, amount, "Transaction description 3");
            trans.EndToEndId = "endToendId2";
            transfert.AddCreditTransfer(trans);

            string result = transfert.AsXmlString();

            Assert.IsTrue(result.Contains("<EndToEndId>endToendId1</EndToEndId>"));
            Assert.IsTrue(result.Contains("<EndToEndId>endToendId2</EndToEndId>"));
        }

        [TestMethod]
        public void ShouldManageMultipleTransactionsTransfer()
        {
            var transfert = new SepaCreditTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 18),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Debtor = new SepaIbanData
                {
                    Bic = Debtor.Bic,
                    Iban = Debtor.Iban,
                    Name = Debtor.Name,
                    Address = new SepaPostalAddress { AdrLine = new List<string>() { "address" } },
                }
            };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.Creditor.Address = new SepaPostalAddress { AdrLine = new List<string>() { "add1", "add2" } };
            trans.EndToEndId = "multiple1";
            transfert.AddCreditTransfer(trans);

            const decimal amount2 = 12.56m;
            trans = CreateTransaction("Transaction Id 2", amount2, "Transaction description 2");
            trans.Creditor.Name = "THEIR_SECOND_NAME";
            trans.Creditor.Address = new SepaPostalAddress { AdrLine = new List<string>() { "add3", "add4" } };
            transfert.AddCreditTransfer(trans);

            const decimal amount3 = 27.35m;

            transfert.AddCreditTransfer(new SepaCreditTransferTransaction
            {
                Id = "Transaction Id 3",
                Creditor = new SepaIbanData
                {
                    Bic = "BANK_BIC",
                    Iban = "ACCOUNT_IBAN_SAMPLE",
                    Name = "NAME"
                },
                Amount = amount3,
                RemittanceInformation = "Transaction description 3"
            });

            const decimal total = (amount + amount2 + amount3)*100;

            Assert.AreEqual(total, transfert.HeaderControlSumInCents);
            Assert.AreEqual(total, transfert.PaymentControlSumInCents);

            Assert.AreEqual(MULTIPLE_ROW_RESULT, transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldValidateThePain00100103XmlSchema()
        {
            var transfert = new SepaCreditTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 18),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Debtor = Debtor
            };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.EndToEndId = "multiple1";
            transfert.AddCreditTransfer(trans);

            const decimal amount2 = 12.56m;
            trans = CreateTransaction("Transaction Id 2", amount2, "Transaction description 2");
            transfert.AddCreditTransfer(trans);

            const decimal amount3 = 27.35m;

            transfert.AddCreditTransfer(new SepaCreditTransferTransaction
            {
                Id = "Transaction Id 3",
                Creditor = new SepaIbanData
                {
                    Bic = "BANK_BIC",
                    Iban = "ACCOUNT_IBAN_SAMPLE",
                    Name = "NAME"
                },
                Amount = amount3,
                RemittanceInformation = "Transaction description 3"
            });

            var validator = XmlValidator.GetValidator(transfert.Schema);
            validator.Validate(transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldValidateThePain00100104XmlSchema()
        {
            var transfert = new SepaCreditTransfer
            {
                CreationDate = new DateTime(2013, 02, 17, 22, 38, 12),
                RequestedExecutionDate = new DateTime(2013, 02, 18),
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me",
                Debtor = Debtor,
                Schema = SepaSchema.Pain00100104
            };

            const decimal amount = 23.45m;
            var trans = CreateTransaction("Transaction Id 1", amount, "Transaction description");
            trans.EndToEndId = "multiple1";
            transfert.AddCreditTransfer(trans);

            const decimal amount2 = 12.56m;
            trans = CreateTransaction("Transaction Id 2", amount2, "Transaction description 2");
            transfert.AddCreditTransfer(trans);

            const decimal amount3 = 27.35m;

            transfert.AddCreditTransfer(new SepaCreditTransferTransaction
            {
                Id = "Transaction Id 3",
                Creditor = new SepaIbanData
                {
                    Bic = "BANK_BIC",
                    Iban = "ACCOUNT_IBAN_SAMPLE",
                    Name = "NAME"
                },
                Amount = amount3,
                RemittanceInformation = "Transaction description 3"
            });

            var validator = XmlValidator.GetValidator(transfert.Schema);
            validator.Validate(transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldRejectNotAllowedXmlSchema()
        {
            try { new SepaCreditTransfer { Schema = SepaSchema.Pain00800102 }; }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("schema is not allowed!"));
            }
        }

        [TestMethod]
        public void ShouldManageOneTransactionTransfer()
        {
            const decimal amount = 23.45m;
            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(amount);

            const decimal total = amount*100;
            Assert.AreEqual(total, transfert.HeaderControlSumInCents);
            Assert.AreEqual(total, transfert.PaymentControlSumInCents);

            Assert.AreEqual(ONE_ROW_RESULT, transfert.AsXmlString());
        }

        [TestMethod]
        public void ShouldRejectIfNoDebtor()
        {
            var transfert = new SepaCreditTransfer
            {
                MessageIdentification = "transferID",
                PaymentInfoId = "paymentInfo",
                InitiatingPartyName = "Me"
            };
            transfert.AddCreditTransfer(CreateTransaction("Transaction Id 1", 100m, "Transaction description"));

            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "The debtor is mandatory.");
            }
        }

        [TestMethod]
        public void ShouldAcceptNoInitiatingPartyName()
        {
            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(100m);
            transfert.InitiatingPartyName = null;

            try { transfert.AsXmlString(); }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ShouldRejectIfNoMessageIdentification()
        {
            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(100m);
            transfert.MessageIdentification = null;
            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "The message identification is mandatory.");
            }
        }

        [TestMethod]
        public void ShouldUseMessageIdentificationAsPaymentInfoIdIfNotDefined()
        {
            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(100m);
            transfert.PaymentInfoId = null;

            string result = transfert.AsXmlString();

            Assert.IsTrue(result.Contains("<PmtInfId>"+ transfert.MessageIdentification + "</PmtInfId>"));
        }

        [TestMethod]
        public void ShouldRejectIfNoTransaction()
        {
            var transfert = new SepaCreditTransfer
                {
                    MessageIdentification = "transferID",
                    PaymentInfoId = "paymentInfo",
                    InitiatingPartyName = "Me",
                    Debtor = Debtor
                };

            try { transfert.AsXmlString(); }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "At least one transaction is needed in a transfer.");
            }
        }

        [TestMethod]
        public void ShouldRejectInvalidDebtor()
        {
            try { new SepaCreditTransfer { Debtor = new SepaIbanData() }; }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "Debtor IBAN data are invalid.");
            }
            
        }

        [TestMethod]
        public void ShouldRejectDebtorWithoutBic()
        {
            var iban = (SepaIbanData)Debtor.Clone();
            iban.UnknownBic = true;

            try { new SepaCreditTransfer { Debtor = iban }; }
            catch (SepaRuleException e)
            {
                Assert.AreEqual(e.Message, "Debtor IBAN data are invalid.");
            }
        }

        [TestMethod]
        public void ShouldRejectNullTransactionTransfer()
        {
            var transfert = new SepaCreditTransfer();

            try { transfert.AddCreditTransfer(null); }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message.Contains("transfer"));
            }
        }

        [TestMethod]
        public void ShouldRejectTwoTransationsWithSameId()
        {
            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(100m);
            transfert.AddCreditTransfer(CreateTransaction("UniqueId", 23.45m, "Transaction description 2"));
            try { transfert.AddCreditTransfer(CreateTransaction("UniqueId", 23.45m, "Transaction description 2")); }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("must be unique in a transfer"));
            }
        }

        [TestMethod]
        public void ShouldRejectTwoTransationsWithSameEndToEndId()
        {
            const decimal amount = 23.45m;

            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(amount);
            var trans = CreateTransaction("Transaction Id 2", 23.45m, "Transaction description 2");
            trans.EndToEndId = "uniqueValue";
            transfert.AddCreditTransfer(trans);
            trans = CreateTransaction("Transaction Id 3", 23.45m, "Transaction description 2");
            trans.EndToEndId = "uniqueValue";
            try { transfert.AddCreditTransfer(trans); }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("must be unique in a transfer"));
            }
        }

        [TestMethod]
        public void ShouldSaveCreditInXmlFile()
        {
            const decimal amount = 23.45m;

            SepaCreditTransfer transfert = GetOneTransactionCreditTransfert(amount);
            transfert.Save(FILENAME);

            var doc = new XmlDocument();
            doc.Load(FILENAME);

            Assert.AreEqual(ONE_ROW_RESULT, doc.OuterXml);
        }


        [TestMethod]
        public void ShouldUseEuroAsDefaultCurrency()
        {
            var transfert = new SepaCreditTransfer();
            Assert.AreEqual("EUR", transfert.DebtorAccountCurrency);
        }
    }
}