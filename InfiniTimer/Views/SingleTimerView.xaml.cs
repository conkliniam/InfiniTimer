using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SingleTimerView : ContentView
{
	public SingleTimerView(SingleTimerSection singleTimerSection)
	{
		InitializeComponent();
		BindingContext = new SingleTimerViewModel(singleTimerSection, Application.Current);
	}
}