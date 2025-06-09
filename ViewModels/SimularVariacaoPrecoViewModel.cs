using BTGPactualBrowniano.app.Models;
using BTGPactualBrowniano.app.ViewModels.Base;
using BTGPactualBrowniano.app.Views.Custom;
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

        private ObservableCollection<CustomEntry> listaEntries;
        public ObservableCollection<CustomEntry> ListaEntries
        {
            get { return listaEntries; }
            set
            {
                listaEntries = value;
                OnPropertyChanged("ListaEntries");
            }
        }

        private bool isFormValid;
        public bool IsFormValid
        {
            get => isFormValid;
            set
            {
                if (isFormValid != value)
                {
                    isFormValid = value;
                    OnPropertyChanged("IsFormValid");
                }
            }
        }

        public ICommand ValidaEntriesCommand { get; }

        public SimularVariacaoPrecoViewModel()
        {
            DadosBrowniano = new DadosBrowniano();
            ListaSeriesDadosBrowniano = new ObservableCollection<DadosBrowniano>();
            ListaEntries = new ObservableCollection<CustomEntry>();

            ValidaEntriesCommand = new Command(validaEntries);
            ValidaEntriesCommand.Execute(null);
        }

        public void validaEntries()
        {
            IsFormValid = ListaEntries.All(e => e.IsValid);
        }
    }
}
