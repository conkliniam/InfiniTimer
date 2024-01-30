using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Models
{
    public class EditTimerModel
    {
        public EditTimerModel(TimerModel timerModel)
        {
            TimerModel = timerModel;
            TimerType = timerModel is SimpleTimerModel
                ? Enum.GetName(typeof(TimerType), Enums.TimerType.Simple)
                : Enum.GetName(typeof(TimerType), Enums.TimerType.Advanced);
            Name = timerModel.Name;
        }

        public string TimerType { get; set; }

        public string Name { get; set; }

        public TimerModel TimerModel { get; set; }
    }
}
