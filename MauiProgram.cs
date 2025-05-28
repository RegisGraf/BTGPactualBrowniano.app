using BTGPactualBrowniano.app.ViewModels;
using BTGPactualBrowniano.app.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BTGPactualBrowniano.app
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterViewModels()
                .RegisterView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            #region ViewModels
            builder.Services.AddSingleton<SimularVariacaoPrecoViewModel>();
            builder.Services.AddSingleton<ColorPickerViewModel>();
            #endregion

            return builder;
        }

        public static MauiAppBuilder RegisterView(this MauiAppBuilder builder)
        {
            #region View
            builder.Services.AddTransient<SimularVariacaoPrecoView>();
            #endregion

            return builder;
        }
    }
}
