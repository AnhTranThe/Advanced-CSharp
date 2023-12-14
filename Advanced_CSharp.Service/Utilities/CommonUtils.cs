using System.Globalization;

namespace Advanced_CSharp.Service.Utilities
{
    public static class CommonUtils
    {

        private static readonly CultureInfo provider = CultureInfo.InvariantCulture;
        /// <summary>
        /// convertToDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime convertToDate(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy", provider);
        }
    }
}
