using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class EditAdvancedTimerView : ContentView
{

    public EditAdvancedTimerView(AdvancedTimerModel advancedTimerModel)
	{
		InitializeComponent();
		BindingContext = new EditAdvancedTimerViewModel(advancedTimerModel, timerContent);
    }
}