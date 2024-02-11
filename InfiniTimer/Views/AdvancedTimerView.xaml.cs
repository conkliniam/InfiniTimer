using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class AdvancedTimerView : ContentView
{
	public AdvancedTimerView(AdvancedTimerModel advancedTimerModel)
	{
		InitializeComponent();
		BindingContext = new AdvancedTimerViewModel(advancedTimerModel, timerContent);
    }
}