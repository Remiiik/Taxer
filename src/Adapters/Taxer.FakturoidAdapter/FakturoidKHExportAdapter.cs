using System;
using System.Collections.Generic;
using System.Linq;
using Altairis.Fakturoid.Client;
using Taxer.Core.Export;
using Taxer.Domain.KH;
using Taxer.Domain.Shared;

namespace Taxer.FakturoidAdapter
{
    public class FakturoidKHExportAdapter : IKHExportSourceAdapter
    {
        private readonly FakturoidConfiguration _connectConfig;

        public FakturoidKHExportAdapter(FakturoidConfiguration connectConfig)
        {
            _connectConfig = connectConfig;
        }

        public KHExportSheet GetExportForPeriod(ExportSetup setup)
        {
            var context = new FakturoidContext(_connectConfig.AccountName, _connectConfig.Login, _connectConfig.Key, "Fakturoid API v2 C#/.NET Client Demo Application (fakturoid@rider.cz)");

            var sheet = new KHExportSheet();

            sheet.ExportData = CreateExportData(context, setup);
            

            return sheet;
        }

        private KHExportBody CreateExportData(FakturoidContext context, ExportSetup setup)
        {
            var data = new KHExportBody
            {
                PeriodInfo = CreatePeriodInfo(context, setup),
                Invoices = CreateInvoices(context, setup),
                InvoicedExpenses = CreateInvoicedExpenses(context, setup),
                SmallExpenses = CreateSummedExpense(context, setup),
                Subject = CreateSubject(context, setup),
            };

            data.Summary = CreateSummary(data);

            return data;
        }

        private KHSummary CreateSummary(KHExportBody data)
        {
            return new KHSummary
            {
                SumInvoicedH = data.Invoices.Sum(s => s.PriceWithoutTaxH),
                SumExpendedH = data.SmallExpenses.PriceWithoutTaxH + data.InvoicedExpenses.Sum(s => s.PriceWithoutTaxH),
                SumInvoicedL = data.Invoices.Sum(s => s.PriceWithoutTaxL),
                SumExpendedL = data.SmallExpenses.PriceWithoutTaxL + data.InvoicedExpenses.Sum(s => s.PriceWithoutTaxL),
            };
        }

        private KHPeriodInfo CreatePeriodInfo(FakturoidContext context, ExportSetup setup)
        {
            return new KHPeriodInfo
            {
                ExportDate = DateTime.Now.Date.ToString("dd.MM.yyyy"),
                Month = setup.ExportMode == ExportMode.Month ? new int?(setup.Period) : null,
                Quarter = setup.ExportMode == ExportMode.Quarter ? new int?(setup.Period) : null,
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

        private List<KHInvoice> CreateInvoices(FakturoidContext context, ExportSetup setup)
        {
            var invoices = context.Invoices.Select().Where(w => w.issued_on >= setup.GetReferenceDate() && w.issued_on < setup.GetEndDate());

            return invoices.Select(s => new KHInvoice
            {
                InvoiceNumber = s.number,
                InvoiceDate = s.issued_on.Value.ToString("dd.MM.yyyy"),
                PartnerVATNumber = s.client_registration_no,
                PriceWithoutTaxH = s.subtotal,
                PriceTaxH = s.total - s.subtotal,

            }).ToList();
        }


        private List<KHInvoicedExpense> CreateInvoicedExpenses(FakturoidContext context, ExportSetup setup)
        {
            var expenses = context.Expenses.Select().Where(w => w.issued_on >= setup.GetReferenceDate() && w.issued_on < setup.GetEndDate() && w.total > 10000).ToList();

            
            return expenses.Select(s => new KHInvoicedExpense
            {
                PriceWithoutTaxH = s.subtotal,
                PriceTaxH = s.total - s.subtotal,

            }).ToList();
        }


        private KHSmallExpenses CreateSummedExpense(FakturoidContext context, ExportSetup setup)
        {
            var expenses = context.Expenses.Select().Where(w => w.issued_on >= setup.GetReferenceDate() && w.issued_on < setup.GetEndDate() && w.total < 10000).ToList();

            return new KHSmallExpenses
            {
                PriceWithoutTaxH = expenses.GetTotalWithoutVAT(21),
                PriceTaxH = expenses.GetVAT(21),
                PriceWithoutTaxL = expenses.GetTotalWithoutVAT(15),
                PriceTaxL = expenses.GetVAT(15),
                //PriceWithoutTaxN = GetTotalWithVAT(expenses, 0),
                //PriceTaxN = GetVAT(expenses, 0),
            };
        }

       
    }
}
