using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services
{
    public interface ITimerService
    {
        public List<TimerModel> GetSavedTimers();
        public bool SaveTimers(List<TimerModel> timers);
    }
}
