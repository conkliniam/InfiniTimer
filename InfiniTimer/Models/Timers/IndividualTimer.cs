using InfiniTimer.Enums;

namespace InfiniTimer.Models.Timers
{
    public class IndividualTimer : TimerSection
    {
        public string DisplayText { get; set; }
        public int Seconds { get; set; }
        public TimerColor Color { get; set; }
    }
}
