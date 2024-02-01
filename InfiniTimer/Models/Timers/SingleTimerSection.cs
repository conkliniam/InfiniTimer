using InfiniTimer.Enums;
using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : ITimerSection
    {
        public SingleTimerSection(int depth)
        {
            Depth = depth;
        }
        public string DisplayText { get; set; }
        public int Seconds { get; set; }
        public TimerColor Color { get; set; }
        public int Depth { get; private set; }
    }
}
