using System;
using System.Collections.Generic;
using System.Linq;
using Altairis.Fakturoid.Client;

namespace Taxer.FakturoidAdapter
{
    internal static class FakturoidExtensions
    {
        internal static int VAT_RATE_REGULAR = 21;
        internal static int VAT_RATE_LOW = 15;

        internal static decimal GetTotalWithoutVAT(this IEnumerable<JsonExpense> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price), 2);
        }

        internal static decimal GetVAT(this IEnumerable<JsonExpense> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price * u.vat_rate / (decimal)100.0), 2);
        }

        internal static decimal GetTotalWithoutVAT(this IEnumerable<JsonInvoice> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price), 2);
        }

        internal static decimal GetVAT(this IEnumerable<JsonInvoice> expenses, int vatRate)
        {
            return Decimal.Round(expenses.SelectMany(s => s.lines).Where(w => w.vat_rate == vatRate).Sum(u => u.quantity * u.unit_price * u.vat_rate / (decimal)100.0), 2);
        }
    }
}