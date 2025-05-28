using BTGPactualBrowniano.app.Models;
using BTGPactualBrowniano.app.ViewModels.Base;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static BTGPactualBrowniano.app.Views.SimularVariacaoPrecoView;

namespace BTGPactualBrowniano.app.ViewModels
{
    public class ColorPickerViewModel : ViewModelBase
    {
        private readonly Popup _popup;
        public ICommand ColorSelectedCommand { get; set; }
        public ICommand CloseCommand { get; set; }

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

        public ColorPickerViewModel(Popup popup)
        {
            _popup = popup;

            ColorSelectedCommand = new Command<Cores>(corSelecionada =>
            {
                WeakReferenceMessenger.Default.Send(new ColorSelectedMessage(corSelecionada));
            });

            CloseCommand = new Command(() =>
            {
                _popup.Close(null);
            });

            ListaCores = new ObservableCollection<Cores>()
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
