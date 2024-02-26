using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;
using Newtonsoft.Json;

namespace InfiniTimer
{
    public partial class EditTimerPage : ContentPage
    {
        public EditTimerPage(TimerModel timerModel,
                             ISavedTimerService savedTimerService,
                             IStagedTimerService stagedTimerService,
                             bool showSave = true)
        {
            InitializeComponent();
            BindingContext = new EditTimerViewModel(timerLayout,
                                                    timerModel,
                                                    savedTimerService,
                                                    stagedTimerService);

            if (showSave)
            {
                stageButton.IsVisible = false;
            }
            else
            {
                saveButton.IsVisible = false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(((EditTimerViewModel)BindingContext).EditTimerModel.TimerModel);
            await DisplayAlert("JSON", json, "Ok");
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            try
            {
                ((EditTimerViewModel)BindingContext).HandleCancel();
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            try
            {
                bool success = ((EditTimerViewModel)BindingContext).HandleSave();

                if (!success)
                {
                    await MessageHelper.ShowFailureMessage("Save Failed");
                }
                else
                {
                    await Navigation.PopAsync(true);
                    await MessageHelper.ShowSuccessMessage("Save Successful");
                }
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private async void StageClicked(object sender, EventArgs e)
        {
            try
            {
                ((EditTimerViewModel)BindingContext).HandleStage();
                await Navigation.PopAsync(true);
                await MessageHelper.ShowSuccessMessage("Stage Successful");
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }
    }
}