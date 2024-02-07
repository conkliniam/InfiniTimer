using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class AlternatingTimerViewModel
    {
        public AlternatingTimerViewModel(AlternatingTimerSection alternatingTimerSection)
        {
            AlternatingTimerSection = alternatingTimerSection;
            CycleOptions = new ObservableCollection<int>(Enumerable.Range(1, AppConstants.CycleLimit));
            NextDepth = AlternatingTimerSection.Depth + 1;
            NextColor = ColorHelper.ThemeColors[NextDepth % 2 == 0 ? ColorHelper.Tertiary : ColorHelper.Primary];
            SetMainTimer = (ITimerSection timerSection) => AlternatingTimerSection.MainTimerSection = timerSection;
            SetAlternateTimer = (ITimerSection timerSection) => AlternatingTimerSection.AlternateTimerSection = timerSection;
        }

        public AlternatingTimerSection AlternatingTimerSection { get; set; }
        public ObservableCollection<int> CycleOptions { get; private set; }
        public Action<ITimerSection> SetMainTimer { get; private set; }
        public Action<ITimerSection> SetAlternateTimer { get; private set; }
        public int NextDepth { get; private set; }
        public Color NextColor { get; private set; }
    }
}
