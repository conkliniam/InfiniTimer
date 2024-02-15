using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;

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
            Console.WriteLine(ex.ToString());
        }
    }

    private async void EditTimerClicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new EditTimerPage((TimerModel)listTimers.SelectedItem, ((TimersViewModel)BindingContext).HandleSaveTimer));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void StageTimerClicked(object sender, EventArgs e)
    {

    }

    private void listTimers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listTimers.SelectedItem != null)
        {
            editButton.IsEnabled = true;
            stageButton.IsEnabled = true;
        }
        else
        {
            editButton.IsEnabled = false;
            stageButton.IsEnabled = false;
        }
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        ((TimersViewModel)BindingContext).HandleDelete();
    }
}