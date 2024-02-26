using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class StagedTimersViewModel
    {
        private readonly IStagedTimerService _stagedTimerService;

        public StagedTimersViewModel(IStagedTimerService stagedTimerService)
        {
            _stagedTimerService = stagedTimerService;

            StagedTimers = _stagedTimerService.GetStagedTimers();
        }

        public ObservableCollection<TimerModel> StagedTimers { get; set; }
    }
}
