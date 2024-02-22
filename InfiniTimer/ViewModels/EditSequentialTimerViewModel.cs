using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditSequentialTimerViewModel
    {
        private readonly StackLayout _timerListLayout;
        private readonly EditTimerContent _timerButtons;

        public EditSequentialTimerViewModel(SequentialTimerSection sequentialTimerSection, StackLayout timerListLayout, EditTimerContent timerButtons)
        {
            SequentialTimerSection = sequentialTimerSection;
            _timerListLayout = timerListLayout;
            _timerButtons = timerButtons;

            if (null == SequentialTimerSection.TimerSections)
            {
                SequentialTimerSection.TimerSections = new ObservableCollection<TimerSection>();
            }

            int nextDepth = SequentialTimerSection.Depth + 1;
            NextColor = ColorHelper.ThemeColors[nextDepth % 2 == 0 ? ColorHelper.Tertiary : ColorHelper.Primary];

            FillTimerLayout();

            timerButtons.SetTimer = (TimerSection timerSection) =>
            {
                if (null != timerSection)
                {
                    SequentialTimerSection.TimerSections.Add(timerSection);
                    AddTimerSection(timerSection);
                }
            };

            timerButtons.Depth = nextDepth;
        }

        public SequentialTimerSection SequentialTimerSection { get; set; }
        public Color NextColor { get; set; }

        private void FillTimerLayout()
        {
            if (SequentialTimerSection.TimerSections.Any())
            {
                foreach (TimerSection timerSection in SequentialTimerSection.TimerSections)
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
                    SequentialTimerSection.TimerSections.Remove(timerSection);

                    if (SequentialTimerSection.TimerSections.Count < AppConstants.ListLimit)
                    {
                        _timerButtons.IsVisible = true;
                    }
                }
            };

            timerContent.TimerSection = timerSection;

            if (SequentialTimerSection.TimerSections.Count >= AppConstants.ListLimit)
            {
                _timerButtons.IsVisible = false;
            }
        }
    }
}
