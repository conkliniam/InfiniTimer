using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer
{
    public partial class EditTimerPage : ContentPage
    {
        public EditTimerPage()
        {
            BindingContext = new EditTimerViewModel();
            InitializeComponent();
        }
        public EditTimerPage(TimerModel timerModel)
        {
            BindingContext = new EditTimerViewModel(timerModel);
            InitializeComponent();
        }
    }
}