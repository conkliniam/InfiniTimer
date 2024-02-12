using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class AlternatingTimerView : ContentView
{
	private readonly Action _handleDelete;

	public AlternatingTimerView(AlternatingTimerSection alternatingTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new AlternatingTimerViewModel(alternatingTimerSection, mainTimerLayout, alternateTimerLayout);

		if (null != handleDelete)
		{
			_handleDelete = handleDelete;
		}
		else
		{
			deleteButton.IsVisible = false;
		}
	}

    private void DeleteButtonClicked(object sender, EventArgs e)
    {
		_handleDelete?.Invoke();
    }
}