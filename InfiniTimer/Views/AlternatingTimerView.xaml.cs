using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class AlternatingTimerView : ContentView
{
	public AlternatingTimerView(AlternatingTimerSection alternatingTimerSection)
	{
		InitializeComponent();
		BindingContext = new AlternatingTimerViewModel(alternatingTimerSection);
		mainTimerLayout.SetTimer = ((AlternatingTimerViewModel)BindingContext).SetMainTimer;
		alternateTimerLayout.SetTimer = ((AlternatingTimerViewModel)BindingContext).SetAlternateTimer;
	}
}