using System.ComponentModel;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Models
{
    public class EditTimerModel : INotifyPropertyChanged
    {
        private string _timerType;
        private TimerModel _timerModel;

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

        public TimerModel TimerModel
        {
            get
            {
                return _timerModel;
            }
            set
            {
                if (value != _timerModel)
                {
                    var old = _timerModel;
                    _timerModel = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= TimerModel_PropertyChange;
                    }

                    if (_timerModel != null)
                    {
                        _timerModel.PropertyChanged += TimerModel_PropertyChange;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void TimerModel_PropertyChange(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("EditTimerModel." + e.PropertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
