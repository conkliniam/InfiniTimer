using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class AlternatingTimerViewModel
    {
        public AlternatingTimerViewModel(AlternatingTimerSection alternatingTimerSection, ViewTimerContent mainSection, ViewTimerContent alternateSection)
        {
            AlternatingTimerSection = alternatingTimerSection;
            
            if (null != AlternatingTimerSection)
            {
                mainSection.TimerSection = AlternatingTimerSection.MainTimerSection;
                alternateSection.TimerSection = AlternatingTimerSection.AlternateTimerSection;
            }
            
        }

        public AlternatingTimerSection AlternatingTimerSection { get; }
    }
}
