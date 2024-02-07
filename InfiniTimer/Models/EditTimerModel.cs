using System.ComponentModel;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Models
{
    public class EditTimerModel : INotifyPropertyChanged
    {
        private string _timerType;
        public EditTimerModel(TimerModel timerModel)
        {
            TimerModel = timerModel;
            TimerType = timerModel is SimpleTimerModel
                ? Enum.GetName(typeof(TimerType), Enums.TimerType.Simple)
                : Enum.GetName(typeof(TimerType), Enums.TimerType.Advanced);
        }

        public string TimerType
        {
            get
            {
                return _timerType;
            }
            set
            {
                if (_timerType != value)
                {
                    _timerType = value;
                    OnPropertyChanged(nameof(TimerType));
                }
            }
        }

        public TimerModel TimerModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
