using System;
using System.Collections.Generic;
using System.Linq;
using Altairis.Fakturoid.Client;
using Taxer.Core.Export;
using Taxer.Domain.KH;
using Taxer.Domain.Shared;

namespace Taxer.FakturoidAdapter
{
    public class FakturoidExportSourceClient : IKHExportSourceAdapter
    {
        public KHExportSheet GetExportForPeriod(ExportSetup setup)
        {
            var context = new FakturoidContext(setup.AccountName, setup.Login, setup.AppKey, "Fakturoid API v2 C#/.NET Client Demo Application (fakturoid@rider.cz)");

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
                Month = setup.Month,
                Year = setup.Year,
            };
        }

        private KHSubject CreateSubject(FakturoidContext context, ExportSetup setup)
        {
            var client = context.GetAccountInfo();

            return new KHSubject
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
                AuthorityOffice2 = setup.OfficeDepartment,
                Phone = client.phone,
            };
        }

        private List<KHInvoice> CreateInvoices(FakturoidContext context, ExportSetup setup)
        {
            var referenceDate = new DateTime(setup.Year, setup.Month, 1);
            var invoices = context.Invoices.Select().Where(w => w.issued_on >= referenceDate && w.issued_on < referenceDate.AddMonths(1));

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
            var referenceDate = new DateTime(setup.Year, setup.Month, 1);

            var expenses = context.Expenses.Select().Where(w => w.issued_on >= referenceDate && w.issued_on < referenceDate.AddMonths(1) && w.total > 10000);

            
            return expenses.Select(s => new KHInvoicedExpense
            {
                PriceWithoutTaxH = s.subtotal,
                PriceTaxH = s.total - s.subtotal,

            }).ToList();
        }


        private KHSmallExpenses CreateSummedExpense(FakturoidContext context, ExportSetup setup)
        {
            var referenceDate = new DateTime(setup.Year, setup.Month, 1);

            var expenses = context.Expenses.Select().Where(w => w.issued_on >= referenceDate && w.issued_on < referenceDate.AddMonths(1) && w.total < 10000);

            return new KHSmallExpenses
            {
                PriceWithoutTaxH = GetTotalWithVAT(expenses, 21),
                PriceTaxH = GetVAT(expenses, 21),
                PriceWithoutTaxL = GetTotalWithVAT(expenses, 15),
                PriceTaxL = GetVAT(expenses, 15),
                //PriceWithoutTaxN = GetTotalWithVAT(expenses, 0),
                //PriceTaxN = GetVAT(expenses, 0),
            };
        }

        private static decimal GetTotalWithVAT(IEnumerable<JsonExpense> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price), 2);
        }

        private static decimal GetVAT(IEnumerable<JsonExpense> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price * u.vat_rate / (decimal)100.0), 2);
        }
    }
}
