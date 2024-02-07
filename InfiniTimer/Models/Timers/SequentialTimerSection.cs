using System.Collections.ObjectModel;

namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : ITimerSection
    {
        public SequentialTimerSection(int depth)
        {
            Depth = depth;
        }
        public ObservableCollection<ITimerSection> TimerSections {  get; set; }
        public int Depth { get; private set; }
    }
}
