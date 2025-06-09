using System.Globalization;
using System.Text.RegularExpressions;

namespace BTGPactualBrowniano.app.Utils
{
    public static class Formatadores
    {
        public static string FormataMoedaReal(string texto)
        {
            string pattern = @"[^\d]";
            string text = Regex.Replace(texto, pattern, "");

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                value /= 100;
                text = value.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            }

            return text;
        }

    }

    public static class Converters
    {
        public static double ConvetStringToDouble(string text, int casasDecimais)
        {
            text = text.Replace("R$", "").Replace(".", "").Replace(",", "").Trim();
            if (double.TryParse(text, out double decimalValue))
            {
                return decimalValue /= 100;
            }

            return 0d;
        }
    }
}
