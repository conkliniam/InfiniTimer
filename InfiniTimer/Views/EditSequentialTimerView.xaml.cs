using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class EditSequentialTimerView : ContentView
{
	private readonly Action _handleDelete;
	public EditSequentialTimerView(SequentialTimerSection sequentialTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new EditSequentialTimerViewModel(sequentialTimerSection, timerListLayout, timerButtons);

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