namespace InfiniTimer.Models.Timers
{
    public class AdvancedTimerModel : TimerModel
    {
        public ITimerSection TimerSection { get; set; }
        public string Description { get; set; }
        public bool AutoContinue { get; set; }
        public bool AutoRepeat { get; set; }
    }
}
