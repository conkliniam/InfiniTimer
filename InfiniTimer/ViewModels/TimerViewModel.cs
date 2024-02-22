using InfiniTimer.Models.Timers;
using InfiniTimer.Views;

namespace InfiniTimer.ViewModels
{
    public class TimerViewModel
    {
        private readonly StackLayout _timerContent;

        public TimerViewModel(TimerModel timerModel, StackLayout timerContent)
        {
            TimerModel = timerModel;
            _timerContent = timerContent;
            FillTimerContent();
        }

        public TimerModel TimerModel{ get; set; }

        private void FillTimerContent()
        {
            _timerContent.Children.Clear();

            if (TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _timerContent.Children.Add(new AdvancedTimerView(advancedTimerModel));
            }
            else if (TimerModel is SimpleTimerModel simpleTimerModel)
            {
                //_timerContent.Children.Add(new SimpleTimerView(simpleTimerModel));
            }
        }
    }
}
