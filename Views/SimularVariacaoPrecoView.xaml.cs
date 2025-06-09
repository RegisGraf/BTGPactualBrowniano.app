using BTGPactualBrowniano.app.Models;
using BTGPactualBrowniano.app.Renderers;
using BTGPactualBrowniano.app.ViewModels;
using BTGPactualBrowniano.app.Views.Custom;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls.Shapes;

namespace BTGPactualBrowniano.app.Views;

public partial class SimularVariacaoPrecoView : ContentPage
{
    private readonly BrownianDrawable _drawable = new();
    SimularVariacaoPrecoViewModel _viewModel;
    List<string> _listaDeCoresJaUsadas = new List<string>();

    public SimularVariacaoPrecoView(SimularVariacaoPrecoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BrownianGraphicsView.Drawable = _drawable;
        LoadDefaultSimulation();
        AlimentaListaEntries();

        BindingContext = _viewModel;
    }

    private void AlimentaListaEntries()
    {
        _viewModel.ListaEntries.Add(customEntryDias);
        _viewModel.ListaEntries.Add(customEntryPreco);
        _viewModel.ListaEntries.Add(customEntryVolatilidade);
        _viewModel.ListaEntries.Add(customEntryRetornoMedio);
    }

    private void LoadDefaultSimulation()
    {
        DadosBrowniano novaSerie = new DadosBrowniano()
        {
            Serie = 1,
            CorDaLinha = _viewModel.DadosBrowniano.CorDaLinha,
            CorDaLinhaHexa = _viewModel.DadosBrowniano.CorDaLinhaHexa,
            NumeroDias = _viewModel.DadosBrowniano.NumeroDias,
            Points = _viewModel.DadosBrowniano.Points,
            PrecoInicial = _viewModel.DadosBrowniano.PrecoInicial,
            RetornoMedio = _viewModel.DadosBrowniano.RetornoMedio,
            Volatilidade = _viewModel.DadosBrowniano.Volatilidade
        };

        _viewModel.ListaSeriesDadosBrowniano.Add(novaSerie);
        AlimentaListaCoresJaUsadas(novaSerie.CorDaLinhaHexa);

        _drawable.CalcularBrownianoFinanceiro(novaSerie.NumeroDias, novaSerie.PrecoInicial, novaSerie.Volatilidade, novaSerie.RetornoMedio, novaSerie.CorDaLinhaHexa, novaSerie.Serie);
    }

    private async void OnGenerateClicked(object sender, EventArgs e)
    {
        if (!CamposPreenchidosValidos())
        {
            await DisplayAlert("Atenção!", RetornaMensgemCamposInvalidos(), "Ok");
            return;
        }

        if (_listaDeCoresJaUsadas.Contains(_viewModel.DadosBrowniano.CorDaLinhaHexa))
        {
            await DisplayAlert("Atenção!", "Esta cor já foi utilizada, por favor escolha outra cor!", "Ok");
            return;
        }

        DadosBrowniano novaSerie = new DadosBrowniano()
        {
            Serie = _viewModel.ListaSeriesDadosBrowniano.Count() == 0 ? 1 : _viewModel.ListaSeriesDadosBrowniano.LastOrDefault().Serie + 1,
            CorDaLinha = _viewModel.DadosBrowniano.CorDaLinha,
            CorDaLinhaHexa = _viewModel.DadosBrowniano.CorDaLinhaHexa,
            NumeroDias = _viewModel.DadosBrowniano.NumeroDias,
            Points = _viewModel.DadosBrowniano.Points,
            PrecoInicial = _viewModel.DadosBrowniano.PrecoInicial,
            RetornoMedio = _viewModel.DadosBrowniano.RetornoMedio,
            Volatilidade = _viewModel.DadosBrowniano.Volatilidade
        };

        AlimentaListaCoresJaUsadas(novaSerie.CorDaLinhaHexa);
        _viewModel.ListaSeriesDadosBrowniano.Add(novaSerie);
        _drawable.CalcularBrownianoFinanceiro(novaSerie.NumeroDias, novaSerie.PrecoInicial, novaSerie.Volatilidade, novaSerie.RetornoMedio, novaSerie.CorDaLinhaHexa, novaSerie.Serie);

        BrownianGraphicsView.Invalidate();
    }

    private void CorTapped(object sender, TappedEventArgs e)
    {
        try
        {
            Rectangle rectangleCor = (Rectangle)sender;

            if (rectangleCor is not null)
            {
                Cores corSelecionada = (Cores)rectangleCor.BindingContext;

                if (corSelecionada is not null)
                {
                    _viewModel.DadosBrowniano.CorDaLinha = corSelecionada.Cor;
                    _viewModel.DadosBrowniano.CorDaLinhaHexa = corSelecionada.CorHexa;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public class ColorSelectedMessage : ValueChangedMessage<Cores>
    {
        public ColorSelectedMessage(Cores cor) : base(cor)
        {
        }
    }

    private async void AbrirListaDeCoresTapped(object sender, TappedEventArgs e)
    {
        try
        {
            var popup = new ColorPickerPopup(new ColorPickerViewModel(new Popup(), _listaDeCoresJaUsadas));

            if (!WeakReferenceMessenger.Default.IsRegistered<ColorSelectedMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<ColorSelectedMessage>(this, (obj, m) =>
                {
                    try
                    {
                        Cores corSelecionada = (Cores)m.Value;

                        if (corSelecionada is not null)
                        {
                            _viewModel.DadosBrowniano.CorDaLinha = corSelecionada.Cor;
                            _viewModel.DadosBrowniano.CorDaLinhaHexa = corSelecionada.CorHexa;

                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        WeakReferenceMessenger.Default.Unregister<ColorSelectedMessage>(this);
                        popup.Close();
                    }
                });
            }

            var selectedColor = await this.ShowPopupAsync(popup);
        }
        catch (Exception)
        {
        }
    }

    private void removerSerieClicked(object sender, EventArgs e)
    {
        try
        {
            ImageButton buttonRemoverSerie = (ImageButton)sender;

            if (buttonRemoverSerie is not null)
            {
                DadosBrowniano serie = (DadosBrowniano)buttonRemoverSerie.BindingContext;

                if (serie is not null)
                {
                    RemoveCorJaUsadaDaLista(serie.CorDaLinhaHexa);
                    _viewModel.ListaSeriesDadosBrowniano.Remove(serie);
                    _drawable.RemoverSerie(serie);
                    BrownianGraphicsView.Invalidate();
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private void AlimentaListaCoresJaUsadas(string corUsada)
    {
        _listaDeCoresJaUsadas.Add(corUsada);
    }

    private void RemoveCorJaUsadaDaLista(string corLiberada)
    {
        _listaDeCoresJaUsadas.Remove(corLiberada);
    }

    private bool CamposPreenchidosValidos()
    {
        return customEntryDias.IsValid && customEntryPreco.IsValid && customEntryRetornoMedio.IsValid && customEntryVolatilidade.IsValid;
    }

    private string RetornaMensgemCamposInvalidos()
    {
        string mensagem = "";

        if (!customEntryDias.IsValid)
            mensagem += "Campo número de dias inválido";

        if (!customEntryPreco.IsValid)
            mensagem += Environment.NewLine + "Campo preço inicial inválido";

        if (!customEntryRetornoMedio.IsValid)
            mensagem += Environment.NewLine + "Campo retorno medio inválido";

        if (!customEntryVolatilidade.IsValid)
            mensagem += Environment.NewLine + "Campo volatilidade inválido";

        return mensagem;
    }
}