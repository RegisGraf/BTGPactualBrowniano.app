using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTGPactualBrowniano.app.Converters
{
    public class CurrencyBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(CurrencyBehavior), 0.0, BindingMode.TwoWay);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private bool _isUpdating;

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
            entry.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
            entry.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = ((BindableObject)sender).BindingContext;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            var entry = (Entry)sender;

            _isUpdating = true;

            // Remove tudo que não for dígito
            string clean = new string(e.NewTextValue?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());

            if (string.IsNullOrEmpty(clean))
            {
                Value = 0;
                entry.Text = string.Empty;
                _isUpdating = false;
                return;
            }

            // Interpreta como centavos
            if (double.TryParse(clean, out double number))
            {
                Value = number / 100.0;
                entry.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Value);
            }

            // Move o cursor para o final
            entry.CursorPosition = entry.Text.Length + 1;

            _isUpdating = false;
        }
    }
}
