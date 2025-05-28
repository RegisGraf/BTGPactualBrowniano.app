using BTGPactualBrowniano.app.Models;
using BTGPactualBrowniano.app.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BTGPactualBrowniano.app.ViewModels
{
    public class SimularVariacaoPrecoViewModel : ViewModelBase
    {
        private DadosBrowniano dadosBrowniano;
        public DadosBrowniano DadosBrowniano
        {
            get { return dadosBrowniano; }
            set
            {
                dadosBrowniano = value;
                OnPropertyChanged("DadosBrowniano");
            }
        }

        private ObservableCollection<DadosBrowniano> listaSeriesBrowniano;
        public ObservableCollection<DadosBrowniano> ListaSeriesDadosBrowniano
        {
            get { return listaSeriesBrowniano; }
            set
            {
                listaSeriesBrowniano = value;
                OnPropertyChanged("ListaSeriesDadosBrowniano");
            }
        }

        private ObservableCollection<Cores> listaCores;
        public ObservableCollection<Cores> ListaCores
        {
            get { return listaCores; }
            set
            {
                listaCores = value;
                OnPropertyChanged("ListaCores");
            }
        }

        public SimularVariacaoPrecoViewModel()
        {
            listaCores = new ObservableCollection<Cores>()
            {
                new Cores() { Cor = Colors.Red, CorHexa = Colors.Red.ToHex() },
                new Cores() { Cor = Colors.Blue, CorHexa = Colors.Blue.ToHex() },
                new Cores() { Cor = Colors.Orange, CorHexa = Colors.Orange.ToHex() },
                new Cores() { Cor = Colors.Black, CorHexa = Colors.Black.ToHex() },
                new Cores() { Cor = Colors.Pink, CorHexa = Colors.Pink.ToHex() },
                new Cores() { Cor = Colors.Purple, CorHexa = Colors.Purple.ToHex() },
                new Cores() { Cor = Colors.Green, CorHexa = Colors.Green.ToHex() },
                new Cores() { Cor = Colors.Brown, CorHexa = Colors.Brown.ToHex() },
                new Cores() { Cor = Colors.Aqua, CorHexa = Colors.Aqua.ToHex() },
                new Cores() { Cor = Colors.Coral, CorHexa = Colors.Coral.ToHex() },
                new Cores() { Cor = Colors.DarkGreen, CorHexa = Colors.DarkGreen.ToHex() },
            };
        }

    }
}
