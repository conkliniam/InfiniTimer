using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class TimerListViewModel
    {
        private readonly StackLayout _timerListLayout;

        public TimerListViewModel(TimerListSection timerListSection, StackLayout timerListLayout)
        {
            TimerListSection = timerListSection;
            _timerListLayout = timerListLayout;
            FillTimerLayout();
        }

        public TimerListSection TimerListSection { get; }

        private void FillTimerLayout()
        {
            _timerListLayout.Children.Clear();

            if (null != TimerListSection &&
                null != TimerListSection.TimerSections &&
                TimerListSection.TimerSections.Any())
            {
                foreach (TimerSection timerSection in TimerListSection.TimerSections)
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
