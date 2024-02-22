using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services
{
    public interface ITimerService
    {
        public List<TimerModel> GetSavedTimers();
        public TimerModel GetSavedTimer(Guid timerId);
        public bool SaveTimer(TimerModel timer);
        public bool SaveTimers(List<TimerModel> timers);
        public bool DeleteTimer(Guid timerId);
        public bool DeleteTimers();
    }
}
