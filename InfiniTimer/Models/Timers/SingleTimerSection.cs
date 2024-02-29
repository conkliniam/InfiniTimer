using InfiniTimer.Enums;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : TimerSection
    {
        public SingleTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public string DisplayText { get; set; }

        public int Seconds { get; set; }

        public TimerColor Color { get; set; }
    }
}
