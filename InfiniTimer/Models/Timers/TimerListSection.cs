using System.Collections.ObjectModel;

namespace InfiniTimer.Models.Timers
{
    public class TimerListSection : TimerSection
    {
        public TimerListSection(int depth = 0)
        {
            Depth = depth;
        }

        public int Cycles { get; set; }

        public ObservableCollection<TimerSection> TimerSections { get; set; }
    }
}
