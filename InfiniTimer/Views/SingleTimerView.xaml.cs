using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SingleTimerView : ContentView
{
	public SingleTimerView(SingleTimerSection singleTimerSection)
	{
		InitializeComponent();
		BindingContext = new SingleTimerViewModel(singleTimerSection, App.Current.Resources);
	}

    private async void PickColorClicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync
			(new ColorPickerView(((SingleTimerViewModel)BindingContext).HandleColorSelection));
    }
}