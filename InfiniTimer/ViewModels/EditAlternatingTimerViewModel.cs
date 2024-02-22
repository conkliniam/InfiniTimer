using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditAlternatingTimerViewModel
    {
        public EditAlternatingTimerViewModel(AlternatingTimerSection alternatingTimerSection, EditTimerContent mainContent, EditTimerContent alternateContent)
        {
            AlternatingTimerSection = alternatingTimerSection;
            CycleOptions = new ObservableCollection<int>(Enumerable.Range(1, AppConstants.CycleLimit));
            int nextDepth = AlternatingTimerSection.Depth + 1;
            NextColor = ColorHelper.ThemeColors[nextDepth % 2 == 0 ? ColorHelper.Tertiary : ColorHelper.Primary];
            mainContent.TimerSection = AlternatingTimerSection.MainTimerSection;
            mainContent.SetTimer = (TimerSection timerSection) => AlternatingTimerSection.MainTimerSection = timerSection;
            mainContent.Depth = nextDepth;
            alternateContent.TimerSection = AlternatingTimerSection.AlternateTimerSection;
            alternateContent.SetTimer = (TimerSection timerSection) => AlternatingTimerSection.AlternateTimerSection = timerSection;
            alternateContent.Depth = nextDepth;
        }

        public AlternatingTimerSection AlternatingTimerSection { get; set; }
        public ObservableCollection<int> CycleOptions { get; private set; }
        public Color NextColor { get; private set; }
    }
}
