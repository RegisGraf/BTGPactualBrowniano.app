using System.Globalization;
using System.Text.RegularExpressions;

namespace BTGPactualBrowniano.app.Converters
{
    public class MoedaConverter : IValueConverter
    {
        public int DecimalPlaces { get; set; } = 2;
        public string CurrencySymbol { get; set; } = "R$";
        public bool AllowNegative { get; set; } = true;  // Permite números negativos
        public double DefaultValue { get; set; } = 0;  // Valor padrão se conversão falhar


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null) return string.Empty;

            //culture ??= CultureInfo.CurrentCulture;

            //if (double.TryParse(value.ToString(), out double numericValue))
            //{
            //    // Remove qualquer caractere não numérico existente
            //    string cleanValue = Regex.Replace(numericValue.ToString($"N{DecimalPlaces}"), @"[^\d.,-]", "");
            //    return cleanValue;
            //}

            //return string.Empty;

            // Converte o valor double para string (apenas números)
            // De double para string
            if (value == null)
                return string.Empty;

            if (value is double doubleValue)
                return doubleValue.ToString(culture);

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            //    return 0d;

            //culture ??= CultureInfo.CurrentCulture;

            //// Remove todos os caracteres não numéricos exceto ponto, vírgula e sinal negativo
            //string cleanValue = Regex.Replace(value.ToString(), @"[^\d.,-]", "");

            //if (double.TryParse(cleanValue, NumberStyles.Any, culture, out double result))
            //{
            //    //var r = Math.Round(result, DecimalPlaces);
            //    result /= (double)Math.Pow(10, DecimalPlaces);
            //    return Math.Round(result, DecimalPlaces);
            //}

            //return 0d;

            // De string para double
            if (value == null)
                return 0.0;

            string input = value.ToString();

            // Remove qualquer caractere que não seja número, vírgula ou ponto
            input = Regex.Replace(input, @"[^0-9.,]", "");

            // Tenta converter para double
            if (double.TryParse(input, NumberStyles.Any, culture, out double result))
                return result;

            return 0.0;
        }
    }
}
