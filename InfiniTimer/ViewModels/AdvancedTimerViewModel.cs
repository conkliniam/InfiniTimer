using InfiniTimer.Models.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class AdvancedTimerViewModel
    {
        public AdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel)
        {
            AdvancedTimerModel = advancedTimerModel;
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set;}
    }
}
