using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class StagedTimerView : ContentView
{
	public StagedTimerView(TimerModel timerModel, ISavedTimerService savedTimerService, IStagedTimerService stagedTimerService)
	{
		InitializeComponent();
		BindingContext = new StagedTimerViewModel(timerModel, currentTimerSection, savedTimerService, stagedTimerService);
	}

    private async void UnstageIconClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerViewModel)BindingContext).UnstageTimer();
            await MessageHelper.ShowSuccessMessage("Timer Unstaged");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void SaveIconClicked(object sender, EventArgs e)
    {
        try
        {
            var success = ((StagedTimerViewModel)BindingContext).SaveTimer();

            if (success)
            {
                await MessageHelper.ShowSuccessMessage("Timer Saved");
            }
            else
            {
                await MessageHelper.ShowFailureMessage("Save Failed");
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void ResetIconClicked(object sender, EventArgs e)
    {
        try
        {
            
            ((StagedTimerViewModel)BindingContext).ResetTimer();
            await MessageHelper.ShowSuccessMessage("Timer Reset");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }
}