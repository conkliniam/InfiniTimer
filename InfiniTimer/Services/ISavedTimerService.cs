using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services
{
    public interface ISavedTimerService
    {
        public List<TimerModel> GetSessionTimers();
        public void ResetTimer(Guid timerId);
        public void AddSessionTimer(TimerModel timer);
        public bool SaveTimer(Guid timerId);
        public bool SaveTimers(List<Guid> timerIds);
        public bool DeleteTimer(Guid timerId);
        public bool DeleteTimers(List<Guid> timerIds);
        public bool DeleteAllTimers();
    }
}
