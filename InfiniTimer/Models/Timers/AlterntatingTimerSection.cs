namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerSection : TimerSection
    {
        public AlternatingTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public TimerSection MainTimerSection { get; set; }

        public TimerSection AlternateTimerSection { get; set; }

        public int Cycles { get; set; }
    }
}
