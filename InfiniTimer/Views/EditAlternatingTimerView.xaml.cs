using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class EditAlternatingTimerView : ContentView
{
	private readonly Action _handleDelete;

	public EditAlternatingTimerView(AlternatingTimerSection alternatingTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new EditAlternatingTimerViewModel(alternatingTimerSection, mainTimerLayout, alternateTimerLayout);

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