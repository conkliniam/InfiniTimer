namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : ITimerSection
    {
        public SequentialTimerSection(int depth)
        {
            Depth = depth;
        }
        public List<ITimerSection> TimerSections {  get; set; }
        public int Depth { get; private set; }
    }
}
