using Microsoft.Extensions.Logging;

namespace InfiniTimer
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
                    fonts.AddFont("Comfortaa-Light.ttf", "ComfortaaLight");
                    fonts.AddFont("Comfortaa-Medium.ttf", "ComfortaaMedium");
                    fonts.AddFont("Comfortaa-Regular.ttf", "ComfortaaRegular");
                    fonts.AddFont("Comfortaa-Semibold.ttf", "ComfortaaSemibold");
                    fonts.AddFont("Comfortaa-Bold.ttf", "ComfortaaBold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}