namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : ITimerSection
    {
        public List<ITimerSection> TimerSections {  get; set; }
    }
}
