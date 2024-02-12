using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SingleTimerView : ContentView
{
	private readonly Action _handleDelete;
	public SingleTimerView(SingleTimerSection singleTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new SingleTimerViewModel(singleTimerSection);
		
		if (null != handleDelete)
		{
            _handleDelete = handleDelete;
        }
		else
		{
			deleteButton.IsVisible = false;
		}
			
	}

    private async void PickColorClicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync
			(new ColorPickerView(((SingleTimerViewModel)BindingContext).HandleColorSelection));
    }

    private void DeleteButtonClicked(object sender, EventArgs e)
    {
		_handleDelete();
    }
}