namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerSection : ITimerSection
    {
        public AlternatingTimerSection(int depth)
        {
            Depth = depth;
        }
        public ITimerSection MainTimerSection { get; set; }
        public ITimerSection AlternateTimerSection { get; set; }
        public int Cycles { get; set; }
        public int Depth { get; private set; }
    }
}
