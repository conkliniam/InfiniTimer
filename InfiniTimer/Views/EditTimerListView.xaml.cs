using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class EditTimerListView : ContentView
{
	private readonly Action _handleDelete;
	public EditTimerListView(TimerListSection listSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new EditTimerListViewModel(listSection, timerListLayout, timerButtons);

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