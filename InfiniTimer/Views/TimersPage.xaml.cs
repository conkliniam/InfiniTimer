using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;
using InfiniTimer.Views;

namespace InfiniTimer;

public partial class TimersPage : ContentPage
{
    private readonly ISavedTimerService _savedTimerService;
    private readonly IStagedTimerService _stagedTimerService;

    public TimersPage(TimersViewModel timersViewModel,
                      ISavedTimerService savedTimerService,
                      IStagedTimerService stagedTimerService)
    {
        InitializeComponent();
        BindingContext = timersViewModel;
        _savedTimerService = savedTimerService;
        _stagedTimerService = stagedTimerService;
    }

    private async void NewTimerClicked(object sender, EventArgs e)
    {
        try
        {
            TimerModel newTimerModel = new SimpleTimerModel();
            _savedTimerService.AddSessionTimer(newTimerModel);
            await Navigation.PushAsync(new EditTimerPage(newTimerModel, _savedTimerService, _stagedTimerService));
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void StageSelectedClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimersViewModel)BindingContext).HandleStageSelected();
            await MessageHelper.ShowSuccessMessage("Timers Staged");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void UnstageSelectedClicked(object sender, EventArgs e)
    {
        try
        {
            ((TimersViewModel)BindingContext).HandleUnstageSelected();
            await MessageHelper.ShowSuccessMessage("Timers Unstaged");
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void DeleteSelectedClicked(object sender, EventArgs e)
    {
        try
        {
            bool success = ((TimersViewModel)BindingContext).HandleDeleteSelected();

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

    private async void DeleteClicked(object sender, EventArgs e)
    {
        try
        {
            bool success = ((TimersViewModel)BindingContext).HandleDeleteAll();

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
            hiddenRow.Height = new GridLength(7, GridUnitType.Star);
            headerRow.Height = new GridLength(2, GridUnitType.Star);
            listRow.Height = new GridLength(15, GridUnitType.Star);
        }
    }

    private async void ListTimersItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listTimers.SelectedItem != null)
        {
            var timer = (TimerModel)listTimers.SelectedItem;

            await Navigation.PushAsync(new TimerView(timer, _savedTimerService, _stagedTimerService));

            listTimers.SelectedItem = null;
        }
    }

    private void SelectAllClicked(object sender, EventArgs e)
    {
        ((TimersViewModel)BindingContext).SelectAll();
    }

    private void UnselectAllClicked(object sender, EventArgs e)
    {
        ((TimersViewModel)BindingContext).UnselectAll();
    }
}