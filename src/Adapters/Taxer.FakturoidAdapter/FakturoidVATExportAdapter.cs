using System;
using System.Collections.Generic;
using System.Linq;
using Altairis.Fakturoid.Client;
using Taxer.Core.Export;
using Taxer.Domain.Shared;
using Taxer.Domain.VAT;

namespace Taxer.FakturoidAdapter
{
    public class FakturoidVATExportAdapter : IVATExportSourceAdapter
    {
        private readonly FakturoidConfiguration _connectConfig;

        public FakturoidVATExportAdapter(FakturoidConfiguration connectConfig)
        {
            _connectConfig = connectConfig;
        }

        public VATExportSheet GetExportForPeriod(ExportSetup setup)
        {
            var context = new FakturoidContext(_connectConfig.AccountName, _connectConfig.Login, _connectConfig.Key, "Fakturoid API v2 C#/.NET Client Demo Application (fakturoid@rider.cz)");

            var sheet = new VATExportSheet();

            sheet.ExportData = CreateExportData(context, setup);
            

            return sheet;
        }

        private VATExportBody CreateExportData(FakturoidContext context, ExportSetup setup)
        {
            var data = new VATExportBody
            {
                PeriodInfo = CreatePeriodInfo(context, setup),
                Invoiced = CreateInvoiced(context, setup),
                Received = CreateReceived(context, setup),
                Adjustments = CreateAdjustments(context, setup),
                Subject = CreateSubject(context, setup),
            };

            data.Summary = CreateSummary(data);

            return data;
        }

        private VATAdjustments CreateAdjustments(FakturoidContext context, ExportSetup setup)
        {
            return new VATAdjustments();
        }

        private VATSummary CreateSummary(VATExportBody data)
        {
            return new VATSummary
            {
                TotalTaxInvoiced = data.Invoiced.TaxH + data.Invoiced.TaxL,
                TotalTaxReceived = data.Received.TaxH + data.Received.TaxL,
            };
        }

        private VATPeriodInfo CreatePeriodInfo(FakturoidContext context, ExportSetup setup)
        {
            return new VATPeriodInfo
            {
                ExportDate = DateTime.Now.Date.ToString("dd.MM.yyyy"),
                Month = setup.Month,
                Year = setup.Year,
            };
        }

        private Subject CreateSubject(FakturoidContext context, ExportSetup setup)
        {
            var client = context.GetAccountInfo();

            return new Subject
            {
                FirstName = client.name.Split(' ').First(),
                Surname = client.name.Split(' ').Last(),
                Country = "ČESKÁ REPUBLIKA",
                City = client.city,
                Street = client.street,
                Email = client.email,
                VATNumber = client.vat_no.Replace("CZ", ""),
                Zip = client.zip,
                AuthorityOfficeNumber = setup.OfficeNo,
                AuthorityOffice2 = setup.OfficeDepartmentNo,
                Phone = client.phone,
            };
        }

        private VATInvoiced CreateInvoiced(FakturoidContext context, ExportSetup setup)
        {
            var referenceDate = new DateTime(setup.Year, setup.Month, 1);
            var invoices = context.Invoices.Select()
                .Where(w => w.issued_on >= referenceDate && w.issued_on < referenceDate.AddMonths(1));

            return new VATInvoiced
            {
                WithoutTaxH = Decimal.Round(invoices.GetTotalWithoutVAT(FakturoidExtensions.VAT_RATE_REGULAR), 0),
                TaxH = Decimal.Round(invoices.GetVAT(FakturoidExtensions.VAT_RATE_REGULAR),0),
                WithoutTaxL = Decimal.Round(invoices.GetTotalWithoutVAT(FakturoidExtensions.VAT_RATE_LOW), 0),
                TaxL = Decimal.Round(invoices.GetVAT(FakturoidExtensions.VAT_RATE_LOW), 0),
            };
        }


        private VATReceived CreateReceived(FakturoidContext context, ExportSetup setup)
        {
            var referenceDate = new DateTime(setup.Year, setup.Month, 1);

            var expenses = context.Expenses.Select().Where(w => w.issued_on >= referenceDate && w.issued_on < referenceDate.AddMonths(1));

            return new VATReceived
            {
                WithoutTaxH = Decimal.Round(expenses.GetTotalWithoutVAT(FakturoidExtensions.VAT_RATE_REGULAR), 0),
                TaxH = Decimal.Round(expenses.GetVAT(FakturoidExtensions.VAT_RATE_REGULAR), 0),
                WithoutTaxL = Decimal.Round(expenses.GetTotalWithoutVAT(FakturoidExtensions.VAT_RATE_LOW), 0),
                TaxL = Decimal.Round(expenses.GetVAT(FakturoidExtensions.VAT_RATE_LOW), 0),
            };
        }
    }
}
