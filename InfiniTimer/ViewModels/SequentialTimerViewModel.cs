using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class SequentialTimerViewModel
    {
        private readonly StackLayout _timerListLayout;

        public SequentialTimerViewModel(SequentialTimerSection sequentialTimerSection, StackLayout timerListLayout)
        {
            SequentialTimerSection = sequentialTimerSection;
            _timerListLayout = timerListLayout;
            FillTimerLayout();
        }

        public SequentialTimerSection SequentialTimerSection { get; }

        private void FillTimerLayout()
        {
            _timerListLayout.Children.Clear();

            if (null != SequentialTimerSection &&
                null != SequentialTimerSection.TimerSections &&
                SequentialTimerSection.TimerSections.Any())
            {
                foreach (TimerSection timerSection in SequentialTimerSection.TimerSections)
                {
                    _timerListLayout.Add(new ViewTimerContent()
                    {
                        TimerSection = timerSection
                    });
                }
            }
        }
    }
}
