using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class SequentialTimerViewModel
    {
        public SequentialTimerViewModel(SequentialTimerSection sequentialTimerSection)
        {
            SequentialTimerSection = sequentialTimerSection;
        }

        public SequentialTimerSection SequentialTimerSection { get; set; }
    }
}
