using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace InfiniTimer
{
    public partial class EditTimerPage : ContentPage
    {
        public EditTimerPage()
        {
            InitializeComponent();
            BindingContext = new EditTimerViewModel(timerLayout);
            
        }
        public EditTimerPage(TimerModel timerModel)
        {
            InitializeComponent();
            BindingContext = new EditTimerViewModel(timerLayout, timerModel);
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(((EditTimerViewModel)BindingContext).EditTimerModel.TimerModel);
            await DisplayAlert("JSON", json, "Ok");
        }
    }
}