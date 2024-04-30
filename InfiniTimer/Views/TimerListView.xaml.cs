using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class TimerListView : ContentView
{
	public TimerListView(TimerListSection timerListSection)
	{
		InitializeComponent();
		BindingContext = new TimerListViewModel(timerListSection, timerListLayout);
	}
}