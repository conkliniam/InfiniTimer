namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerModel : TimerModel
    {
        public TimerGroup TimerGroup { get; set; }
        public string Description { get; set; }
        public bool AutoContinue { get; set; }
        public bool AutoRepeat { get; set; }
    }
}
