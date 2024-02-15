using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services
{
    public interface ITimerService
    {
        public List<TimerModel> GetSavedTimers();
        public bool SaveTimer(TimerModel timer);

        public bool DeleteTimers();
    }
}
