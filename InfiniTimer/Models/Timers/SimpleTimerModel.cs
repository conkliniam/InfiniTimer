namespace InfiniTimer.Models.Timers
{
    public class SimpleTimerModel : TimerModel
    {
        public SimpleTimerModel(bool ignoreChanges = true)
        {
            IgnoreChanges = ignoreChanges;
        }

        public SingleTimerSection Timer { get; set; }
    }
}
