namespace InfiniTimer.Models.Timers
{
    public class TimerGroup : TimerSection
    {
        public TimerSection MainTimerSection { get; set; }
        public TimerSection AlternateTimerSection { get; set; }
        public int Cycles { get; set; }
    }
}
