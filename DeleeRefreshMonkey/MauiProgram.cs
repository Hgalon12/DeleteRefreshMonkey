using DeleeRefreshMonkey.ViewModels;
using DeleeRefreshMonkey.Views;
using Microsoft.Extensions.Logging;

namespace DeleeRefreshMonkey
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<MonkeyDetailsViewModel>();
            builder.Services.AddTransient<MonkeyViewModel>();
            builder.Services.AddTransient<MonkeyView>();
            builder.Services.AddTransient<MonkeyDetailsView>();


            return builder.Build();
        }
    }
}