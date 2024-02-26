using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.Services
{
    public interface IStagedTimerService
    {
        public ObservableCollection<TimerModel> GetStagedTimers();
        public void StageTimer(TimerModel timer, bool sendMessage = true);
        public void UnstageTimer(TimerModel timer, bool sendMessage = true);
        public void StageTimers(List<TimerModel> timers);
        public void UnstageTimers(List<TimerModel> timers);
        public void UnstageAllTimers();
    }
}
