using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Models
{
    public class EditTimerModel : CommonBase
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
                    RaisePropertyChanged(nameof(TimerType));
                }
            }
        }

        public TimerModel TimerModel { get; set; }
    }
}
