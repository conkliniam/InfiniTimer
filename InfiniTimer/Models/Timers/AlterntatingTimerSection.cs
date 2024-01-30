namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerSection : ITimerSection
    {
        public ITimerSection MainTimerSection { get; set; }
        public ITimerSection AlternateTimerSection { get; set; }
        public int Cycles { get; set; }
    }
}
