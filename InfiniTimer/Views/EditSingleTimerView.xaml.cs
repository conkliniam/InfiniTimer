using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class EditSingleTimerView : ContentView
{
	private readonly Action _handleDelete;

    public EditSingleTimerView(SingleTimerSection singleTimerSection, Action handleDelete = null)
	{
		InitializeComponent();
		BindingContext = new EditSingleTimerViewModel(singleTimerSection);
		
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
			(new ColorPickerView(((EditSingleTimerViewModel)BindingContext).HandleColorSelection));
    }

    private async void PickSoundClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync
            (new SoundPickerView(((EditSingleTimerViewModel)BindingContext).HandleSoundSelection));
    }

    private void DeleteButtonClicked(object sender, EventArgs e)
    {
		_handleDelete();
    }
}