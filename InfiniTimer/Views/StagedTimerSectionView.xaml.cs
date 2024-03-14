using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class StagedTimerSectionView : ContentView
{
	public StagedTimerSectionView(TimerSection parentSection)
	{
		InitializeComponent();
		BindingContext = new StagedTimerSectionViewModel(parentSection, Application.Current, currentTimerView);
	}

    private async void BackArrowClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerSectionViewModel)BindingContext).GoBack();
        }
        catch(Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void ForwardArrowClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerSectionViewModel)BindingContext).GoForward();
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void StartClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerSectionViewModel)BindingContext).StartTimer();
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void PauseClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerSectionViewModel)BindingContext).PauseTimer();
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        try
        {
            ((StagedTimerSectionViewModel)BindingContext).CancelTimer();
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }
}