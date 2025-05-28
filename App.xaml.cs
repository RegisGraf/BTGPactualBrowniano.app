using BTGPactualBrowniano.app.ViewModels;
using BTGPactualBrowniano.app.Views;

namespace BTGPactualBrowniano.app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new SimularVariacaoPrecoView(new SimularVariacaoPrecoViewModel()));
        }
    }
}