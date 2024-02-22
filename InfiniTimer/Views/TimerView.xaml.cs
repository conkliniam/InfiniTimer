using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class TimerView : ContentPage
{
	public TimerView(TimerModel timerModel)
	{
		InitializeComponent();
        BindingContext = new TimerViewModel(timerModel, timerContent);
	}

    private void EditClicked(object sender, EventArgs e)
    {

    }

    private async void StageClicked(object sender, EventArgs e)
    {
        ((TimerViewModel)BindingContext).TimerModel.IsStaged = true;
        await MessageHelper.ShowSuccessMessage("Timer Staged Successfully");
    }

    private async void UnstageClicked(object sender, EventArgs e)
    {
        ((TimerViewModel)BindingContext).TimerModel.IsStaged = false;
        await MessageHelper.ShowSuccessMessage("Timer Unstaged Successfully");
    }

    private void SaveClicked(object sender, EventArgs e)
    {

    }

    private void ResetClicked(object sender, EventArgs e)
    {

    }
}