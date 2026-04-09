using System;

namespace AutomechanicsProject.Classes
{
    public static class CurrencyManager
    {
        public static string SelectedCurrency { get; private set; } = "RUB";
        public static decimal CurrentRate { get; private set; } = 1m;

        public static void SetCurrency(string currencyCode, decimal rate)
        {
            SelectedCurrency = currencyCode;
            CurrentRate = rate;
        }

        public static decimal ConvertPrice(decimal priceInRub)
        {
            if (SelectedCurrency == "RUB")
                return priceInRub;

            return priceInRub * CurrentRate;
        }

        public static decimal ConvertPriceToRub(decimal priceInCurrency, string currencyCode, decimal rate)
        {
            if (currencyCode == "RUB")
                return priceInCurrency;

            return priceInCurrency / rate;
        }
    }
}