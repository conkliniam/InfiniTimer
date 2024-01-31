using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SequentialTimerView : ContentView
{
	public SequentialTimerView(SequentialTimerSection sequentialTimerSection)
	{
		InitializeComponent();
		BindingContext = new SequentialTimerViewModel(sequentialTimerSection);
	}
}