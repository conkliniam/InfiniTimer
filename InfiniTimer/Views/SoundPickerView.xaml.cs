using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models;
using InfiniTimer.ViewModels;

namespace InfiniTimer.Views;

public partial class SoundPickerView : ContentPage
{
	public SoundPickerView(Action<TimerSound, string> HandleSoundSelection)
	{
		InitializeComponent();
        BindingContext = new SoundPickerViewModel(HandleSoundSelection);
    }

    private async void CancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void SoundSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listSounds.SelectedItem != null)
        {
            var sound = listSounds.SelectedItem as SoundPickerModel;
            ((SoundPickerViewModel)BindingContext).OnChangeSound(sound.Sound, sound.DisplayName);
            Navigation.PopModalAsync();
        }
    }

    private async void OnPlayPauseButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var soundPickerModel = ((ImageButton)sender).CommandParameter as SoundPickerModel;

            if (null != soundPickerModel)
            {
                ((SoundPickerViewModel)BindingContext).PlayPauseSound(soundPickerModel);
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }

    private async void OnStopButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var soundPickerModel = ((ImageButton)sender).CommandParameter as SoundPickerModel;

            if (null != soundPickerModel)
            {
                ((SoundPickerViewModel)BindingContext).StopSound(soundPickerModel);
            }
        }
        catch (Exception ex)
        {
            await MessageHelper.HandleException(ex);
        }
    }
}