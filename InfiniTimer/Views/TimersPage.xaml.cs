using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;
using InfiniTimer.Views;

namespace InfiniTimer;

public partial class TimersPage : ContentPage
{
    public TimersPage(TimersViewModel timersViewModel)
    {
        InitializeComponent();
        BindingContext = timersViewModel;
    }

    private async void NewTimerClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new EditTimerPage(null, ((TimersViewModel)BindingContext).HandleSaveTimer));
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void EditTimerClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync
                (new EditTimerPage
                    ((TimerModel)((ImageButton)sender).CommandParameter,
                     ((TimersViewModel)BindingContext).HandleSaveTimer));
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void StageTimerClicked(object sender, EventArgs e)
    {

    }

    private async void DeleteSelectedClicked(object sender, EventArgs e)
    {

    }

    private async void DeleteClicked(object sender, EventArgs e)
    {
        try
        {
            bool success = ((TimersViewModel)BindingContext).HandleDelete();

            if (success)
            {
                await MessageHelper.ShowSuccessMessage("Delete Successful");
            }
            else
            {
                await MessageHelper.ShowFailureMessage("Delete Failed");
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void AddDefaultClicked(object sender, EventArgs e)
    {
        try
        {
            bool success = ((TimersViewModel)BindingContext).HandleAddDefault();

            if (success)
            {
                await MessageHelper.ShowSuccessMessage("Add Successful");
            }
            else
            {
                await MessageHelper.ShowFailureMessage("Add Failed");
            }
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
            TimerModel timerModel = (TimerModel)((ImageButton)sender).CommandParameter;

            if (timerModel != null)
            {
                bool success = ((TimersViewModel)BindingContext).HandleSaveTimer(timerModel);

                if (success)
                {
                    await MessageHelper.ShowSuccessMessage("Save Successful");
                }
                else
                {
                    await MessageHelper.ShowFailureMessage("Save Failed");
                }
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
            TimerModel timerModel = (TimerModel)((ImageButton)sender).CommandParameter;

            if (timerModel != null)
            {
                bool success = ((TimersViewModel)BindingContext).HandleResetTimer(timerModel);

                if (success)
                {
                    await MessageHelper.ShowSuccessMessage("Reset Successful");
                }
                else
                {
                    await MessageHelper.ShowFailureMessage("Reset Failed");
                }
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private void OptionsIconClicked(object sender, EventArgs e)
    {
        if (actionButtons.IsVisible)
        {
            actionButtons.IsVisible = false;
            hiddenRow.Height = 0;
            headerRow.Height = new GridLength(1, GridUnitType.Star);
            listRow.Height = new GridLength(11, GridUnitType.Star);
            
        }
        else
        {
            actionButtons.IsVisible = true;
            hiddenRow.Height = new GridLength(5, GridUnitType.Star);
            headerRow.Height = new GridLength(2, GridUnitType.Star);
            listRow.Height = new GridLength(17, GridUnitType.Star);
        }
    }

    private async void ListTimersItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listTimers.SelectedItem != null)
        {
            await Navigation.PushAsync(new TimerView((TimerModel)listTimers.SelectedItem));
            listTimers.SelectedItem = null;
        }
    }
}