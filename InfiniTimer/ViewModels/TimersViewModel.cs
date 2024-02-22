using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Views;
using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class TimersViewModel : CommonBase
    {
        private readonly ITimerService _timerService;

        public ObservableCollection<TimerModel> TimerModels { get; set; }

        public TimersViewModel(ITimerService timerService)
        {
            _timerService = timerService;
            TimerModels = new ObservableCollection<TimerModel>(_timerService.GetSavedTimers());
        }

        public bool HandleSaveTimer(TimerModel timerModel)
        {
            timerModel.IsDirty = false;
            
            var success = _timerService.SaveTimer(timerModel);

            if (!TimerModels.Contains(timerModel))
            {
                TimerModels.Add(timerModel);
            }

            timerModel.IgnoreChanges = false;

            return success;
        }

        public bool HandleResetTimer(TimerModel timerModel)
        {
            bool success = false;
            TimerModels.Remove(timerModel);

            timerModel = _timerService.GetSavedTimer(timerModel.Id);

            if (null != timerModel)
            {
                TimerModels.Add(timerModel);
                success = true;
            }

            return success;
        }

        public bool HandleDeleteTimer(TimerModel timerModel)
        {
            bool success = _timerService.DeleteTimer(timerModel.Id);
            TimerModels.Remove(timerModel);

            timerModel = _timerService.GetSavedTimer(timerModel.Id);

            if (null != timerModel)
            {
                TimerModels.Add(timerModel);
                success = true;
            }

            return success;
        }

        public bool HandleDelete()
        {
            var success = _timerService.DeleteTimers();
            TimerModels.Clear();
            return success;
        }

        public bool HandleAddDefault()
        {
            List<TimerModel> timers = TimerHelper.GetDefaultTimers();
            
            foreach (TimerModel timerModel in timers)
            {
                TimerModels.Add(timerModel);
            }

            return _timerService.SaveTimers(timers);
        }
    }
}
