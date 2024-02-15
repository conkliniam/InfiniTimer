using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;
using Newtonsoft.Json;

namespace InfiniTimer
{
    public partial class EditTimerPage : ContentPage
    {
        private readonly Func<TimerModel, bool> _handleSave;
        private readonly Action<TimerModel> _handleStage;

        public EditTimerPage(TimerModel timerModel = null, Func<TimerModel, bool> handleSave = null, Action<TimerModel> handleStage = null)
        {
            InitializeComponent();
            BindingContext = new EditTimerViewModel(timerLayout, timerModel);

            if (handleSave is null)
            {
                saveButton.IsVisible = false;
            }
            else
            {
                _handleSave = handleSave;
            }

            if (handleStage is null)
            {
                stageButton.IsVisible = false;
            }
            else
            {
                _handleStage = handleStage;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(((EditTimerViewModel)BindingContext).EditTimerModel.TimerModel);
            await DisplayAlert("JSON", json, "Ok");
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            bool success = false;

            if (_handleSave != null)
            {
                success = _handleSave.Invoke(((EditTimerViewModel)BindingContext).EditTimerModel.TimerModel);
            }

            if (!success)
            {
                await DisplayAlert("Save Failed", "Unable to save changes, please fix any errors and try again.", "Ok");
            }
            else
            {
                await Navigation.PopAsync(true);
            }
        }

        private void StageClicked(object sender, EventArgs e)
        {
            var timerModel = ((EditTimerViewModel)BindingContext).EditTimerModel.TimerModel;
            timerModel.IsDirty = true;
            _handleStage?.Invoke(timerModel);
        }
    }
}