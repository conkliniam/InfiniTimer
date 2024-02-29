namespace InfiniTimer.Models.Timers
{
    public class AdvancedTimerModel : TimerModel
    {
        public AdvancedTimerModel(bool ignoreChanges = true)
        {
            IgnoreChanges = ignoreChanges;
        }

        public TimerSection TimerSection { get; set; }

        public string Description { get; set; }

        public bool AutoContinue { get; set; }

        public bool AutoRepeat { get; set; }
    }
}
