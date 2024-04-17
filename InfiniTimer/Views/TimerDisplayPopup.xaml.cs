using CommunityToolkit.Maui.Views;
using InfiniTimer.Models;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class TimerDisplayPopup : Popup
{
	public TimerDisplayPopup(TimerDisplayModel currentTimer, TimerDisplayModel nextTimer)
	{
		InitializeComponent();
        BindingContext = new TimerDisplayPopupViewModel(currentTimer, nextTimer);
	}

    async void OnYesButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(true, cts.Token);
    }

    async void OnNoButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(false, cts.Token);
    }
}