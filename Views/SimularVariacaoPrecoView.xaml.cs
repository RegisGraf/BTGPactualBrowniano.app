using BTGPactualBrowniano.app.Models;
using BTGPactualBrowniano.app.Renderers;
using BTGPactualBrowniano.app.ViewModels;
using BTGPactualBrowniano.app.Views.Custom;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;

namespace BTGPactualBrowniano.app.Views;

public partial class SimularVariacaoPrecoView : ContentPage
{
    private readonly BrownianDrawable _drawable = new();
    SimularVariacaoPrecoViewModel _viewModel;

    public SimularVariacaoPrecoView(SimularVariacaoPrecoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _viewModel.DadosBrowniano = new DadosBrowniano();
        _viewModel.ListaSeriesDadosBrowniano = new ObservableCollection<DadosBrowniano>();
        BrownianGraphicsView.Drawable = _drawable;
        LoadDefaultSimulation();

        BindingContext = _viewModel;
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

        _drawable.CalcularBrownianoFinanceiro(novaSerie.NumeroDias, novaSerie.PrecoInicial, novaSerie.Volatilidade, novaSerie.RetornoMedio, novaSerie.CorDaLinhaHexa, novaSerie.Serie);
    }

    private void OnGenerateClicked(object sender, EventArgs e)
    {
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
            var popup = new ColorPickerPopup(new ColorPickerViewModel(new Popup()));

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

            if(buttonRemoverSerie is not null)
            {
                DadosBrowniano serie = (DadosBrowniano)buttonRemoverSerie.BindingContext;

                if (serie is not null)
                {
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
}