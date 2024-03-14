using InfiniTimer.Enums;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : TimerSection
    {
        public SingleTimerSection(int depth = 0)
        {
            Depth = depth;
            Sound = TimerSound.Ringing1;
            Vibrate = true;
        }

        public string DisplayText { get; set; }

        public int Seconds { get; set; }

        public TimerColor Color { get; set; }

        public TimerSound Sound { get; set; }

        public bool Vibrate { get; set; }
    }
}
