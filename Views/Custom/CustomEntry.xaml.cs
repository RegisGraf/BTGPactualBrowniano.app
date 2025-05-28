using System.Globalization;
using System.Text.RegularExpressions;
using BTGPactualBrowniano.app.Utils;

namespace BTGPactualBrowniano.app.Views.Custom;

public partial class CustomEntry : ContentView
{
    #region Bindable Properties
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(string), typeof(CustomEntry), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry), string.Empty);

    public static readonly BindableProperty KeyboardTypeProperty =
        BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(CustomEntry), Keyboard.Default);

    public static readonly BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CustomEntry), true);

    public static readonly BindableProperty TipoEntryProperty =
        BindableProperty.Create(nameof(TipoEntry), typeof(TiposEntry), typeof(CustomEntry), TiposEntry.Default, propertyChanged: OnTipoEntryChanged);

    public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(CustomEntry), false);

    public static readonly BindableProperty CasasDecimaisProperty =
        BindableProperty.Create(nameof(CasasDecimais), typeof(int), typeof(CustomEntry), 2, validateValue: (bindable, value) => (int)value >= 0);

    public static readonly BindableProperty AplicarValidacaoProperty =
        BindableProperty.Create(nameof(AplicarValidacao), typeof(bool), typeof(CustomEntry), false);

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomEntry), default(double));

    public static readonly BindableProperty MaxLengthProperty =
        BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(CustomEntry), int.MaxValue);

    public static readonly BindableProperty MinLengthProperty =
        BindableProperty.Create(nameof(MinLength), typeof(int), typeof(CustomEntry), int.MinValue);

    public static readonly BindableProperty ExibeMensagemErroProperty =
        BindableProperty.Create(nameof(ExibeMensagemErro), typeof(bool), typeof(CustomEntry), false);

    #endregion

    #region Properties
    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Keyboard KeyboardType
    {
        get => (Keyboard)GetValue(KeyboardTypeProperty);
        set => SetValue(KeyboardTypeProperty, value);
    }

    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public TiposEntry TipoEntry
    {
        get => (TiposEntry)GetValue(TipoEntryProperty);
        set => SetValue(TipoEntryProperty, value);
    }

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public bool ExibeMensagemErro
    {
        get => (bool)GetValue(ExibeMensagemErroProperty);
        set => SetValue(ExibeMensagemErroProperty, value);
    }

    public int CasasDecimais
    {
        get => (int)GetValue(CasasDecimaisProperty);
        set => SetValue(CasasDecimaisProperty, value);
    }

    public bool AplicarValidacao
    {
        get => (bool)GetValue(AplicarValidacaoProperty);
        set => SetValue(AplicarValidacaoProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public int MinLength
    {
        get => (int)GetValue(MinLengthProperty);
        set => SetValue(MinLengthProperty, value);
    }
    #endregion

    public CustomEntry()
    {
        InitializeComponent();
    }

    private static void OnTipoEntryChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var customEntry = (CustomEntry)bindable;
            customEntry.AplicarFormatacaoPorTipo();
        }
        catch (Exception ex)
        {
        }
    }

    private void AplicarFormatacaoPorTipo()
    {
        switch (TipoEntry)
        {
            case TiposEntry.Numerico:
                internalEntry.Keyboard = Keyboard.Numeric;
                internalEntry.TextChanged += FormatNumeric;
                break;

            case TiposEntry.Moeda:
                internalEntry.Keyboard = Keyboard.Numeric;
                internalEntry.TextChanged += FormatCurrency;
                break;

            case TiposEntry.Texto:
                internalEntry.Keyboard = Keyboard.Default;
                internalEntry.TextChanged += FormatDefault;
                break;
            case TiposEntry.Default:
                internalEntry.Keyboard = Keyboard.Default;
                internalEntry.TextChanged += FormatDefault;
                break;
            default:
                internalEntry.Keyboard = Keyboard.Default;
                internalEntry.TextChanged += FormatDefault;
                break;
        }
    }

    #region Métodos de Formatação
    private void FormatDefault(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (AplicarValidacao)
                ValidateInput();
        }
        catch (Exception ex)
        {
        }
    }

    private void FormatNumeric(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        string text = CleanInput(e.NewTextValue);

        if (!string.IsNullOrWhiteSpace(text) && !text.All(char.IsDigit))
        {
            entry.Text = CleanInput(entry.Text);
            return;
        }

        if (double.TryParse(text, out double value))
        {
            value /= (double)Math.Pow(10, CasasDecimais);

            string formatString = $"N{CasasDecimais}";

            string formattedValue = value.ToString(formatString, CultureInfo.GetCultureInfo("pt-BR"));
            string decimalValue = formattedValue;

            Value = decimalValue;
            entry.Text = formattedValue;

            CursorToEnd(entry, formattedValue);

            if (AplicarValidacao)
                ValidateInput();
        }
    }

    private void FormatCurrency(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

        var entry = (Entry)sender;

        if (string.IsNullOrEmpty(entry?.Text))
            return;

        entry.Text = Formatadores.FormataMoedaReal(entry.Text);

        // Move o cursor para o final
        CursorToEnd(entry);

        if (AplicarValidacao)
            ValidateInput();
    }

    private void CursorToEnd(Entry entry, string formattedValue = "")
    {
        switch (TipoEntry)
        {
            case TiposEntry.Numerico:
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        // Caso não tenha unidade, posiciona no final do número
                        if (CasasDecimais > 0)
                        {
                            int decimalPos = formattedValue.IndexOf(',');
                            if (decimalPos >= 0)
                            {
                                // Posiciona após os decimais
                                entry.CursorPosition = decimalPos + CasasDecimais + 1;
                                return;
                            }
                        }

                        // Padrão: final do texto
                        entry.CursorPosition = formattedValue.Length;
                    }
                    catch { }
                });
                break;
            default:
                Device.BeginInvokeOnMainThread(() =>
                {
                    try { entry.CursorPosition = entry.Text?.Length ?? 0; } catch { }
                });
                break;
        }
    }
    #endregion

    #region Validações
    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(Value) || string.IsNullOrWhiteSpace(Value))
        {
            IsValid = false;
            ExibeMensagemErro = false;
            return false;
        }

        switch (TipoEntry)
        {
            case TiposEntry.Texto:
                IsValid = Value.Length >= 2 && Value.Length >= MinLength && Value.Length <= MaxLength;
                ExibeMensagemErro = !IsValid;
                lblMensagem.Text = "Campo inválido";
                break;
            case TiposEntry.Numerico:
                IsValid = Value.Length >= MinLength && Value.Length <= MaxLength;
                ExibeMensagemErro = !IsValid;
                lblMensagem.Text = "Campo inválido";
                break;
            case TiposEntry.Moeda:
                IsValid = double.Parse(CleanInput(Value)) > 0;
                ExibeMensagemErro = !IsValid;
                lblMensagem.Text = "Preço inicial deve ser maior que 0";
                break;
            default:
                break;
        }
        return IsValid;
    }

    private string CleanInput(string input)
    {
        string pattern = CasasDecimais > 0
            ? @"[^\d-]"
            : @"[^\d-]";
        return Regex.Replace(input, pattern, "");
    }
    #endregion
}