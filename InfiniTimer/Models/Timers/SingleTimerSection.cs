using InfiniTimer.Enums;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : ITimerSection
    {
        public string DisplayText { get; set; }
        public int Seconds { get; set; }
        public TimerColor Color { get; set; }
    }
}
