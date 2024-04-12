using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace InfiniTimer.Common
{
    public static class MessageHelper
    {
        public static async Task ShowSuccessMessage(string message, View visualElement = null)
        {
            await ShowMessage(message, ColorHelper.TimerColors[Enums.TimerColor.Green].Dark, Colors.White, visualElement: visualElement);
        }

        public static async Task ShowFailureMessage(string message, View visualElement = null)
        {
            await ShowMessage(message, ColorHelper.TimerColors[Enums.TimerColor.Red].Dark, Colors.White, visualElement: visualElement);
        }

        public static async Task HandleException(Exception exception)
        {
            await ShowMessage(exception.Message, ColorHelper.TimerColors[Enums.TimerColor.Red].Dark, Colors.White);
        }

        public static async Task ShowMessage(string message, Color backgroundColor, Color textColor, double seconds = 3, View visualElement = null)
        {
            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = backgroundColor,
                TextColor = textColor,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(14),
                CharacterSpacing = 0.5
            };


            TimeSpan duration = TimeSpan.FromSeconds(seconds);

            if (null != visualElement)
            {
                await visualElement.DisplaySnackbar(message, null, "OK", duration, snackbarOptions);
            }
            else
            {
                var snackbar = Snackbar.Make(message, null, "OK", duration, snackbarOptions);
                await snackbar.Show(cancellationTokenSource.Token);
            }
        }
    }
}
