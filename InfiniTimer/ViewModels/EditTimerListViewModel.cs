using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditTimerListViewModel
    {
        private readonly StackLayout _timerListLayout;
        private readonly EditTimerContent _timerButtons;

        public EditTimerListViewModel(TimerListSection timerListSection, StackLayout timerListLayout, EditTimerContent timerButtons)
        {
            TimerListSection = timerListSection;
            _timerListLayout = timerListLayout;
            _timerButtons = timerButtons;

            if (null == TimerListSection.TimerSections)
            {
                TimerListSection.TimerSections = new ObservableCollection<TimerSection>();
            }

            int nextDepth = TimerListSection.Depth + 1;
            NextColor = ColorHelper.ThemeColors[nextDepth % 2 == 0 ? ColorHelper.Tertiary : ColorHelper.Primary];

            FillTimerLayout();

            timerButtons.SetTimer = (TimerSection timerSection) =>
            {
                if (null != timerSection)
                {
                    TimerListSection.TimerSections.Add(timerSection);
                    AddTimerSection(timerSection);
                }
            };

            timerButtons.Depth = nextDepth;
            CycleOptions = new ObservableCollection<int>(Enumerable.Range(1, AppConstants.CycleLimit));
        }

        public TimerListSection TimerListSection { get; set; }
        public Color NextColor { get; set; }
        public ObservableCollection<int> CycleOptions { get; private set; }

        private void FillTimerLayout()
        {
            if (TimerListSection.TimerSections.Any())
            {
                foreach (TimerSection timerSection in TimerListSection.TimerSections)
                {
                    AddTimerSection(timerSection);
                }
            }
        }

        private void AddTimerSection(TimerSection timerSection)
        {
            var timerContent = new EditTimerContent
            {
                Depth = timerSection.Depth,
                BackgroundColor = NextColor
            };

            _timerListLayout.Children.Add(timerContent);

            timerContent.SetTimer = (TimerSection section) =>
            {
                if (section is null)
                {
                    _timerListLayout.Children.Remove(timerContent);
                    TimerListSection.TimerSections.Remove(timerSection);

                    if (TimerListSection.TimerSections.Count < AppConstants.ListLimit)
                    {
                        _timerButtons.IsVisible = true;
                    }
                }
            };

            timerContent.TimerSection = timerSection;

            if (TimerListSection.TimerSections.Count >= AppConstants.ListLimit)
            {
                _timerButtons.IsVisible = false;
            }
        }
    }
}
