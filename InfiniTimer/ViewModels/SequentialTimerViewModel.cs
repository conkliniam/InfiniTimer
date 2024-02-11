using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class SequentialTimerViewModel
    {
        private readonly StackLayout _timerListLayout;
        private readonly TimerContent _timerButtons;

        public SequentialTimerViewModel(SequentialTimerSection sequentialTimerSection, StackLayout timerListLayout, TimerContent timerButtons)
        {
            SequentialTimerSection = sequentialTimerSection;
            _timerListLayout = timerListLayout;
            _timerButtons = timerButtons;

            if (null == SequentialTimerSection.TimerSections)
            {
                SequentialTimerSection.TimerSections = new ObservableCollection<ITimerSection>();
            }

            int nextDepth = SequentialTimerSection.Depth + 1;
            NextColor = ColorHelper.ThemeColors[nextDepth % 2 == 0 ? ColorHelper.Tertiary : ColorHelper.Primary];

            timerButtons.SetTimer = (ITimerSection timerSection) =>
            {
                if (null != timerSection)
                {
                    SequentialTimerSection.TimerSections.Add(timerSection);
                    var timerContent = new TimerContent
                    {
                        TimerSection = timerSection,
                        Depth = timerSection.Depth,
                        BackgroundColor = NextColor
                    };
                    _timerListLayout.Children.Add(timerContent);

                    if (SequentialTimerSection.TimerSections.Count >= AppConstants.ListLimit)
                    {
                        _timerButtons.IsVisible = false;
                    }
                }
            };

            timerButtons.Depth = nextDepth;
        }

        public SequentialTimerSection SequentialTimerSection { get; set; }
        public Color NextColor { get; set; }
    }
}
