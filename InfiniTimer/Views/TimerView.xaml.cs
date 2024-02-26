using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class TimerView : ContentPage
{
    public TimerView(TimerModel timerModel,
                     ISavedTimerService savedTimerService,
                     IStagedTimerService stagedTimerService)
	{
		InitializeComponent();
        BindingContext = new TimerViewModel(timerModel,
                                            timerContent,
                                            savedTimerService,
                                            stagedTimerService);
    }

    private async void EditClicked(object sender, EventArgs e)
    {
        try
        {
            var editPage = ((TimerViewModel)BindingContext).GetEditPage();
            await Navigation.PushAsync(editPage);
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void StageClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimerViewModel)BindingContext).HandleStageTimer();
            
            await MessageHelper.ShowSuccessMessage("Stage Successful");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
        
    }

    private async void UnstageClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimerViewModel)BindingContext).HandleUnstageTimer();
            await MessageHelper.ShowSuccessMessage("Unstage Successful");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void SaveClicked(object sender, EventArgs e)
    {
        try
        {
            var success = ((TimerViewModel)BindingContext).HandleSaveTimer();

            if (success)
            {
                await MessageHelper.ShowSuccessMessage("Save Successful");
            }
            else
            {
                await MessageHelper.ShowSuccessMessage("Save Failed");
            }
        }
        catch (Exception ex )
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void ResetClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimerViewModel)BindingContext).HandleResetTimer();
            await MessageHelper.ShowSuccessMessage("Reset Successful");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void CopyClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimerViewModel)BindingContext).Copy();
            await MessageHelper.ShowSuccessMessage("Copy Successful");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void DeleteClicked(object sender, EventArgs e)
    {
        try
        {
            var success = ((TimerViewModel)BindingContext).HandleDeleteTimer();

            if (success)
            {
                await Navigation.PopAsync();
                await MessageHelper.ShowSuccessMessage("Delete Successful");
            }
            else
            {
                await MessageHelper.ShowSuccessMessage("Delete Failed");
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }
}