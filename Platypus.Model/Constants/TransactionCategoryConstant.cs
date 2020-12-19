using System.Collections.Generic;
using System.Linq;

namespace Platypus.Model.Constants {

    public static class TransactionCategoryConstant {
        public static readonly string Shopping = "SHP";
        public static readonly string Bills = "BIL";
        public static readonly string General = "GEN";
        public static readonly string Transport = "TRN";
        public static readonly string EatingOut = "EOT";
        public static readonly string Groceries = "GRO";
        public static readonly string Holidays = "HOL";
        public static readonly string Entertainment = "ENT";
        public static readonly string Charity = "CHR";
        public static readonly string Expenses = "EXP";
        public static readonly string Family = "FAM";
        public static readonly string Finances = "FIN";
        public static readonly string Gifts = "GIF";
        public static readonly string PersonalCare = "PCA";

        public static string GetDescription(string value) {
            return !string.IsNullOrEmpty(value) && ValuesAndDescriptions.ContainsKey(value) ? ValuesAndDescriptions[value] : null;
        }

        public static string GetValue(string description) {
            return ValuesAndDescriptions
                .Where(v => v.Value == description).Select(v => v.Key).FirstOrDefault();
        }

        public static Dictionary<string, string> ValuesAndDescriptions {
            get {
                return new Dictionary<string, string> {
                    {Shopping, "Shopping" },
                    {Bills, "Bills" },
                    {General, "General" },
                    {Transport, "Transport" },
                    {EatingOut, "Eating Out" },
                    {Groceries, "Groceries" },
                    {Holidays, "Holidays" },
                    {Entertainment, "Entertainment" },
                    {Charity, "Charity" },
                    {Expenses, "Expenses" },
                    {Family, "Family" },
                    {Finances, "Finances" },
                    {Gifts, "Gifts" },
                    {PersonalCare, "Personal Care" },
                };
            }
        }
    }
}