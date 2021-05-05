using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using FS.Ubl2.Udt;
using FS.Ubl2.Cac;

namespace FS.Ubl2
{
    public class CreateInvoice
    {
        public void Create(string xmlFilename, string moneda, string facturaId, 
            DateTime fechaFactura, string notas, string empresaid, string empresa, 
            string calle, string poblacion, string codigoPostal, string cliente,
            string calleCliente, string poblacionCliente, string codigoPostalCliente, 
            string cifCliente, decimal iva, decimal totalIva, decimal totalImporteNeto, 
            decimal totalImporteBruto, decimal totalFactura, FS.Ubl2.Cac.InvoiceLineType[] lineas)
        {
            FS.Ubl2.InvoiceType invoice = CreateInvoiceData(moneda, facturaId, fechaFactura, 
                notas, empresaid, empresa, calle, poblacion, codigoPostal, cliente, calleCliente, 
                poblacionCliente, codigoPostalCliente, cifCliente, iva, totalIva, totalImporteNeto,
                totalImporteBruto, totalFactura, lineas);

            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = FSLibrary.TextUtil.ControlChars.Tab.ToString();
            
            using (XmlWriter writer = XmlWriter.Create(xmlFilename, setting))
            {
                Type typeToSerialize = typeof(FS.Ubl2.InvoiceType);
                XmlSerializer xs = new XmlSerializer(typeToSerialize);
                xs.Serialize(writer, invoice);
            }
            
            Console.WriteLine("Invoice written to:\n{0}", new FileInfo(xmlFilename).FullName );
        }

        private static FS.Ubl2.InvoiceType CreateInvoiceData(string moneda, 
            string facturaId, DateTime fechaFactura, string notas, string empresaid, 
            string empresa, string calle, string poblacion, string codigoPostal,
            string cliente,string calleCliente, string poblacionCliente, 
            string codigoPostalCliente, string cifCliente, 
            decimal iva, decimal totalIva, decimal totalImporteNeto, 
            decimal totalImporteBruto, decimal totalFactura, FS.Ubl2.Cac.InvoiceLineType[] lineas)
        {
            // Default that shpould be set when you load the library. Don't need to set it for each document.
            FS.Ubl2.UblBaseDocumentType.GlbCustomizationID =
                "urn:tradeshift.com:ubl-2.0-customizations:2010-06";
            FS.Ubl2.UblBaseDocumentType.GlbProfileID =
                "urn:www.cenbii.eu:profile:bii04:ver1.0";

            // Default value assinged to all amounts in this thread
            AmountType.TlsDefaultCurrencyID = moneda;


            // This initialization will only work with C# 3.0 and above
            FS.Ubl2.InvoiceType res = new FS.Ubl2.InvoiceType
            {
                // UBLVersionID = "2.0", Don't need to set this one. hardcoded in the library
                ID = facturaId,
                CopyIndicator = false,
                //UUID = "849FBBCE-E081-40B4-906C-94C5FF9D1AC3",
                IssueDate = fechaFactura,
                InvoiceTypeCode = "380",
                Note = new TextType[] { notas },
                //TaxPointDate = new DateTime(2016, 1, 1),
                //OrderReference = new OrderReferenceType
                //{
                //    ID = "AEG012345",
                //   SalesOrderID = "CON0095678",
                //   //UUID = "6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1",
                //   IssueDate = new DateTime(2005, 6, 20)
                //},
                AccountingSupplierParty = new SupplierPartyType
                {
                    //CustomerAssignedAccountID = "CO001",
                    Party = new PartyType
                    {
                        PartyName = new PartyNameType[] { empresa },
                        PostalAddress = new AddressType
                        {
                            StreetName = calle,
                            //BuildingName = "38-BAJO",
                            //BuildingNumber = "56A",
                            CityName = poblacion,
                            PostalZone = codigoPostal,
                            //CountrySubentity = "Heremouthshire",
                            //AddressLine = new AddressLineType[] { "The Roundabout" },
                            Country = new CountryType { IdentificationCode = "ES" }
                        },
                        PartyTaxScheme = new PartyTaxSchemeType[]
                        {
                            new PartyTaxSchemeType
                            {
                                //RegistrationName = "Farthing Purchasing Consortia",
                                CompanyID = cifCliente,
                                //ExemptionReason = "N/A",
                                TaxScheme = new TaxSchemeType
                                {
                                    Name = "VAT"
                                    //ID = "VAT",
                                    //TaxTypeCode = "VAT"
                                }
                            }
                        },
                        Contact = new ContactType
                        {
                            Name = empresaid + " " + empresaid,
                            //Telephone = "0158 1233714",
                            //Telefax = "0158 1233856",
                            //ElectronicMail = "bouquet@fpconsortial.co.uk",
                        },
                        Person = new PersonType
                        {
                            FirstName = empresa,
                            FamilyName = empresa
                        }
                    }
                },
                AccountingCustomerParty = new CustomerPartyType
                {
                    //CustomerAssignedAccountID = "XFB01",
                    //SupplierAssignedAccountID = "GT00978567",
                    Party = new PartyType
                    {
                        PartyIdentification =  new PartyIdentificationType[] {
                            cifCliente
                        },
                        PartyName = new PartyNameType[] { cliente },
                        PostalAddress = new AddressType
                        {
                            StreetName = calleCliente,
                            //BuildingName = "Thereabouts",
                            //BuildingNumber = "56A",
                            CityName = poblacionCliente,
                            PostalZone = codigoPostalCliente,
                            //CountrySubentity = "Avon",
                            //AddressLine = new AddressLineType[] { "3rd Floor, Room 5" },
                            Country = new CountryType { IdentificationCode = "ES" }
                        },
                        PartyTaxScheme = new PartyTaxSchemeType[]
                        {
                            new PartyTaxSchemeType
                            {
                                //RegistrationName = "Bridgtow District Council",
                                CompanyID = cifCliente,
                                //ExemptionReason = "Local Authority",
                                TaxScheme = new TaxSchemeType { ID = "ES VAT", TaxTypeCode="VAT" }
                            }
                        },
                        //Contact = new ContactType
                        //{
                        //    Name = "Mr Fred Churchill",
                        //    Telephone = "0127 2653214",
                        //    Telefax = "0127 2653215",
                        //    ElectronicMail = "fred@iytcorporation.gov.uk"
                        //}
                    }
                },
                Delivery = new DeliveryType[]
                {
                    new DeliveryType
                    {
                        //ActualDeliveryDate = new DateTime(2005,6,20),
                        ////ActualDeliveryTime = new DateTime(DateTime.Parse("11:30:00.0Z", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal).Ticks, DateTimeKind.Utc),
                        //ActualDeliveryTime = new DateTime(2005,6,20,11,30,00,DateTimeKind.Utc), // MS DateTime xml serializing have a bug! Won't serialize time as utc, always local. 
                        //DeliveryAddress = new AddressType
                        //{
                        //    StreetName = "Avon Way",
                        //    BuildingName = "Thereabouts",
                        //    BuildingNumber = "56A",
                        //    CityName = "Bridgtow",
                        //    PostalZone = "ZZ99 1ZZ",
                        //    CountrySubentity = "Avon",
                        //    AddressLine = new AddressLineType[] { "3rd Floor, Room 5" },
                        //    Country = new CountryType { IdentificationCode = "GB"}
                        //}
                    }
                },
                //PaymentMeans = new PaymentMeansType[]
                //{
                //    new PaymentMeansType
                //    {
                //        PaymentMeansCode = "20",
                //        PaymentDueDate = new DateTime(2005,7,21),
                //        PayeeFinancialAccount = new FinancialAccountType
                //        {
                //            ID = "12345678",
                //            Name = "Farthing Purchasing Consortia",
                //            AccountTypeCode = "Current",
                //            CurrencyCode = "GBP",
                //            FinancialInstitutionBranch = new BranchType
                //            {
                //                ID = "10-26-58",
                //                Name = "Open Bank Ltd, Bridgstow Branch ",
                //                FinancialInstitution = new FinancialInstitutionType
                //                {
                //                    ID = "10-26-58",
                //                    Name = "Open Bank Ltd",
                //                    Address = new AddressType
                //                    {
                //                        StreetName = "City Road",
                //                        BuildingName = "Banking House",
                //                        BuildingNumber = "12",
                //                        CityName = "London",
                //                        PostalZone = "AQ1 6TH",
                //                        CountrySubentity = "London",
                //                        AddressLine = new AddressLineType[] {  "5th Floor" },
                //                        Country = new CountryType { IdentificationCode = "GB" }
                //                    }
                //                },
                //                Address = new AddressType
                //                {
                //                    StreetName = "Busy Street",
                //                    BuildingName = "The Mall",
                //                    BuildingNumber = "152",
                //                    CityName = "Farthing",
                //                    PostalZone = "AA99 1BB",
                //                    CountrySubentity = "Heremouthshire",
                //                    AddressLine = new AddressLineType[] { "West Wing" },
                //                    Country = new CountryType {  IdentificationCode = "GB" }
                //                }
                //            },
                //            Country = new CountryType { IdentificationCode = "GB" }
                //        }
                //    }
                //},
                //PaymentTerms = new PaymentTermsType[]
                //{
                //    new PaymentTermsType
                //    {
                //        Note = new TextType[] { "Payable within 1 calendar month from the invoice date" },
                //    }
                //},
                //AllowanceCharge = new AllowanceChargeType[]
                //{
                //    new AllowanceChargeType
                //    {
                //        ChargeIndicator = false,
                //        AllowanceChargeReasonCode = "17",
                //        MultiplierFactorNumeric = 0.10M,
                //        Amount = 10.00M
                //    }
                //},
                TaxTotal = new TaxTotalType[]
                {
                    new TaxTotalType
                    {
                        TaxAmount = iva,
                        TaxEvidenceIndicator = true,
                        TaxSubtotal = new TaxSubtotalType[]
                        {
                            new TaxSubtotalType
                            {
                                TaxableAmount = totalIva,
                                TaxAmount = iva,
                                TaxCategory = new TaxCategoryType
                                {
                                    ID = "A",
                                    TaxScheme = new TaxSchemeType { ID = "ES IVA", TaxTypeCode = "VAT"}
                                }
                            }
                        }
                    }
                },
                LegalMonetaryTotal = new MonetaryTotalType
                {
                    LineExtensionAmount = totalImporteNeto,
                    TaxExclusiveAmount = totalIva,
                    AllowanceTotalAmount = totalImporteBruto,
                    PayableAmount = totalFactura
                },
                InvoiceLine = lineas
                //InvoiceLine = new InvoiceLineType[]
                //{
                //    new InvoiceLineType
                //    {
                //        ID = "1",
                //        InvoicedQuantity = new QuantityType{ unitCode="KG", Value=100 },
                //        LineExtensionAmount = 100.00M,
                //        OrderLineReference = new OrderLineReferenceType[]
                //        {
                //            new OrderLineReferenceType
                //            {
                //                LineID = "1",
                //                SalesOrderLineID = "A",
                //                LineStatusCode = "NoStatus",
                //                OrderReference = new OrderReferenceType
                //                {
                //                    ID = "AEG012345",
                //                    SalesOrderID = "CON0095678",
                //                    UUID = "6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1",
                //                    IssueDate = new DateTime(2005,6,20)
                //                }
                //            }
                //        },
                //        TaxTotal = new TaxTotalType[]
                //        {
                //            new TaxTotalType
                //            {
                //                TaxAmount = 17.50M,
                //                TaxEvidenceIndicator = true,
                //                TaxSubtotal = new TaxSubtotalType[]
                //                {
                //                    new TaxSubtotalType
                //                    {
                //                        TaxableAmount = 100.00M,
                //                        TaxAmount = 17.50M,
                //                        TaxCategory = new TaxCategoryType 
                //                        {
                //                            ID = "A",
                //                            Percent = 17.5M,
                //                            TaxScheme = new TaxSchemeType { ID = "UK VAT", TaxTypeCode = "VAT"}
                //                        }
                //                    }
                //                }
                //            }
                //        },
                //        Item = new ItemType
                //        {
                //            Description = new TextType[] { "Acme beeswax" },
                //            Name = "beeswax",
                //            BuyersItemIdentification = new ItemIdentificationType { ID = "6578489" },
                //            SellersItemIdentification = new ItemIdentificationType { ID = "17589683" },
                //            ItemInstance = new ItemInstanceType[]
                //            {
                //                new ItemInstanceType
                //                {
                //                    LotIdentification = new LotIdentificationType { LotNumberID = "546378239", ExpiryDate = new DateTime(2010,1,1) }
                //                }
                //            }
                //        },
                //        Price = new PriceType
                //        {
                //            PriceAmount = 1.00M,
                //            BaseQuantity = new QuantityType{ unitCode="KG", Value = 1 }
                //        }
                //    }
                //}
            };

            return res;
        }
    }
}
