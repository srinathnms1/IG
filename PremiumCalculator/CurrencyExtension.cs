namespace PremiumCalculator
{
    using System;

    public static class CurrencyExtension
    {
        public static decimal ToFixed(this decimal value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        public static string ToFixed1(this string value, int decimals)
        {
            return value;
        }
    }
}
