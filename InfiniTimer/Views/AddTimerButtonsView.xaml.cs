using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class AddTimerButtonsView : ContentView
{
	public AddTimerButtonsView(Action HandleAddTimer, Action HandleAddList, Action HandleAddAlternates)
	{
		InitializeComponent();
		BindingContext = new AddTimerButtonsViewModel(HandleAddTimer, HandleAddList, HandleAddAlternates);
	}

    private void AddTimerClicked(object sender, EventArgs e)
    {
		((AddTimerButtonsViewModel)BindingContext).AddTimer();
    }

    private void AddTimerListClicked(object sender, EventArgs e)
    {
        ((AddTimerButtonsViewModel)BindingContext).AddList();
    }

    private void AddTimerAlternatesClicked(object sender, EventArgs e)
    {
        ((AddTimerButtonsViewModel)BindingContext).AddAlternates();
    }
}