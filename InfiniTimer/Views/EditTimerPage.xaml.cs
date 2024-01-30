using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

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
    }
}