using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class AlternatingTimerViewModel
    {
        public AlternatingTimerViewModel(AlternatingTimerSection alternatingTimerSection)
        {
            AlternatingTimerSection = alternatingTimerSection;
        }

        public AlternatingTimerSection AlternatingTimerSection { get; set; }
    }
}
