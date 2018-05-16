using Taxer.Core.Export;
using Taxer.Domain.KH;

namespace Taxer.InMemoryAdapter
{
    public class DummyExportSourceAdapter : IKHExportSourceAdapter
    {

        public KHExportSheet GetExportForPeriod(ExportSetup setup)
        {
            var sheet = new KHExportSheet();

            sheet.SoftwareName = "EPO MF ČR";
            sheet.SoftwareVersion = "39.21.1";
            sheet.ExportData = new KHExportBody();
            sheet.ExportData.Version = "02.01";
            sheet.ExportData.PeriodInfo = CreatePeriodInfo();
            sheet.ExportData.Subject = CreateSubjectInfo();

            sheet.ExportData.Invoices.Add(CreateLine1());
            sheet.ExportData.Invoices.Add(CreateLine2());
            sheet.ExportData.Invoices.Add(CreateLine3());

            sheet.ExportData.Summary = CreateSummary();

            sheet.Footer = CreateFooter();

            return sheet;
        }

        private KHExportFooter CreateFooter()
        {
            return new KHExportFooter
            {
                FileInfo = new FileInfo
                {
                    Length = 1105,
                    CheckSum = "ae39764090e9e43f5a32079faab24f23",
                    FileName = "DPHKH1 - 8606094255 - 20170905 - 214348",
                    AuthorityOfficeNumber = "461",
                },
                User = new KHUser(),
            };
        }

        private KHSummary CreateSummary()
        {
            return new KHSummary
            {
                SumInvoicedH = 209888,
            };
        }

        private KHInvoice CreateLine1()
        {
            return new KHInvoice
            {
                PriceTaxH = 12705,
                PriceWithoutTaxH = 60500,
                InvoiceDate = "07.08.2017",
                InvoiceNumber = "20170012",
                LineNumber = 3,
                PartnerVATNumber = "3563467",
            };
        }

        private KHInvoice CreateLine2()
        {
            return new KHInvoice
            {
                PriceTaxH = 13650,
                PriceWithoutTaxH = 65000,
                InvoiceDate = "20.08.2017",
                InvoiceNumber = "20170013",
                LineNumber = 2,
                PartnerVATNumber = "3134154",
            };
        }

        private KHInvoice CreateLine3()
        {
            return new KHInvoice
            {
                PriceTaxH = 18666,
                PriceWithoutTaxH = 88888,
                InvoiceDate = "25.08.2017",
                InvoiceNumber = "20170009",
                LineNumber = 1,
                PartnerVATNumber = "34151512",
            };
        }

        private KHSubject CreateSubjectInfo()
        {
            return new KHSubject
            {
                Country = "ČESKÁ REPUBLIKA",
                Street = "street",
                StreetNo = "32",
                Zip = "zip00",
                StreetNoOfficial = "streetno",
                VATNumber = "vatnumber",
                Email = "email@gmail.com",
                FirstName = "FirstName",
                Surname = "Surname",
                AuthorityOfficeNumber = "finurad",
                City = "BRNO-XXXXX",
            };
        }

        private KHPeriodInfo CreatePeriodInfo()
        {
            return new KHPeriodInfo
            {
                Year = 2017,
                Month = 8,
                ExportDate = "05.09.2017",
            };
        }
    }
}
