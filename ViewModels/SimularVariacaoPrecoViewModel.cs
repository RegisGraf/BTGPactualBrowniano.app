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

                if(listaSeriesBrowniano != null && listaSeriesBrowniano.Any())
                {
                    if (ListaCoresJaUsadas == null)
                        ListaCoresJaUsadas = new ObservableCollection<string>();

                    ListaCoresJaUsadas.Add(listaSeriesBrowniano?.LastOrDefault()?.CorDaLinhaHexa);
                }
            }
        }

        private ObservableCollection<string> listaCoresJaUsadas;
        public ObservableCollection<string> ListaCoresJaUsadas
        {
            get { return listaCoresJaUsadas; }
            set
            {
                listaCoresJaUsadas = value;
                OnPropertyChanged("ListaCoresJaUsadas");
            }
        }

        public SimularVariacaoPrecoViewModel()
        {
            DadosBrowniano = new DadosBrowniano();
            ListaSeriesDadosBrowniano = new ObservableCollection<DadosBrowniano>();
        }

    }
}
