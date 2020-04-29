using System;

namespace cd.Domain.Infrastructure
{
    public static class IntExtensionMethods
    {
        public static bool IsBetween(this int source, int lowValue, int highValue)
        {
            return lowValue <= source && source <= highValue;
        }

        public static void ValidateRange(this int source, int startValue, int endValue, string errorMessage)
        {
            if (!source.IsBetween(startValue, endValue))
            {
                throw new ArgumentOutOfRangeException(errorMessage);
            }
        }

        /// <summary>
        /// Pluralizes the string representation of the int value.
        /// </summary>
        /// <returns>string</returns>
        public static string Pluralize(this int value)
        {
            if (value <= 0) return value.ToString();

            switch (value % 100)
            {
                case 11:
                case 12:
                case 13:
                    return value + "th";
            }

            switch (value % 10)
            {
                case 1:
                    return value + "st";
                case 2:
                    return value + "nd";
                case 3:
                    return value + "rd";
                default:
                    return value + "th";
            }
        }
    }
}