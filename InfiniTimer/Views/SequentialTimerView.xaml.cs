using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SequentialTimerView : ContentView
{
	private readonly Action _handleDelete;
	public SequentialTimerView(SequentialTimerSection sequentialTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new SequentialTimerViewModel(sequentialTimerSection, timerListLayout, timerButtons);

		if (handleDelete != null )
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