using System.Collections.ObjectModel;

namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : TimerSection
    {
        public SequentialTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public ObservableCollection<TimerSection> TimerSections { get; set; }
    }
}
