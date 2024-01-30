using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer;

public partial class TimersPage : ContentPage
{
    public TimersPage(TimersViewModel timersViewModel)
    {
        InitializeComponent();
        BindingContext = timersViewModel;
    }

    private async void New_Timer_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new EditTimerPage());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void Edit_Timer_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new EditTimerPage((TimerModel)listTimers.SelectedItem));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void Stage_Timer_Clicked(object sender, EventArgs e)
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
}