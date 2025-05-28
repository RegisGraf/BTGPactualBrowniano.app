using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BTGPactualBrowniano.app.Utils
{
    public static class Formatadores
    {
        public static string FormataMoedaReal(string texto)
        {
            string text = texto.Replace("R$", "").Replace(".", "").Replace(",", "").Trim();

            if (!double.TryParse(text, out double value))
            {
                return string.Empty;
            }

            value /= 100.0; // Converte centavos para reais
            text = value.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));

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
