using InfiniTimer.Models;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class ColorPickerView : ContentPage
{
	public ColorPickerView(Action<string, Color, Color> HandleColorSelection)
	{
		InitializeComponent();
		BindingContext = new ColorPickerViewModel(HandleColorSelection);
	}

    private async void CancelClicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private void ColorSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listColors.SelectedItem != null)
        {
            var color = listColors.SelectedItem as ColorPickerModel;
            ((ColorPickerViewModel)BindingContext).OnChangeColor(color.Text, color.BackgroundColor, color.ForegroundColor);
            Navigation.PopModalAsync();
        }
    }
}