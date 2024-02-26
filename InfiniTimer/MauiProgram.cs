using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;
using InfiniTimer.Common;

namespace InfiniTimer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Comfortaa-Light.ttf", AppConstants.ComfortaaLight);
                    fonts.AddFont("Comfortaa-Medium.ttf", AppConstants.ComfortaaMedium);
                    fonts.AddFont("Comfortaa-Regular.ttf", AppConstants.ComfortaaRegular);
                    fonts.AddFont("Comfortaa-Semibold.ttf", AppConstants.ComfortaaSemibold);
                    fonts.AddFont("Comfortaa-Bold.ttf", AppConstants.ComfortaaBold);
                })
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<ISavedTimerService, SavedTimerService>();
            mauiAppBuilder.Services.AddSingleton<IStagedTimerService, StagedTimerService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<TimersViewModel>();
            mauiAppBuilder.Services.AddTransient<StagedTimersViewModel>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<TimersPage>();

            return mauiAppBuilder;
        }
    }
}