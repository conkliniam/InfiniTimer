using InfiniTimer.Common;
using InfiniTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class TimerDisplayPopupViewModel : CommonBase
    {
        public TimerDisplayPopupViewModel(TimerDisplayModel currentTimer, TimerDisplayModel nextTimer)
        {
            CurrentTimer = currentTimer;
            NextTimer = nextTimer;
        }

        public TimerDisplayModel CurrentTimer { get; }
        public TimerDisplayModel NextTimer { get; }
    }
}
