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
    public class TimersViewModel : INotifyPropertyChanged
    {
        private readonly ITimerService _timerService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TimerModel> TimerModels { get; set; }

        public TimersViewModel(ITimerService timerService)
        {
            _timerService = timerService;
            TimerModels = new ObservableCollection<TimerModel>(_timerService.GetSavedTimers());
            TimerHelper.AddDefaultTimers(TimerModels);

        }

        public bool HandleSaveTimer(TimerModel timerModel)
        {
            timerModel.IgnoreChanges = true;
            timerModel.IsDirty = false;
            var success = _timerService.SaveTimer(timerModel);

            if (!TimerModels.Contains(timerModel))
            {
                TimerModels.Add(timerModel);
            }

            return success;
        }

        public bool HandleDelete()
        {
            var success = _timerService.DeleteTimers();
            TimerModels.Clear();
            TimerHelper.AddDefaultTimers(TimerModels);
            return success;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
