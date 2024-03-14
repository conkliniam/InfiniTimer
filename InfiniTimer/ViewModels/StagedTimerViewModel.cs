using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.ViewModels
{
    public class StagedTimerViewModel
    {
        private readonly VerticalStackLayout _currentTimerSection;
        private readonly IStagedTimerService _stagedTimerService;
        private readonly ISavedTimerService _savedTimerService;

        public StagedTimerViewModel(TimerModel timerModel,
                                    VerticalStackLayout currentTimerSection,
                                    ISavedTimerService savedTimerService,
                                    IStagedTimerService stagedTimerService)
        {
            TimerModel = timerModel;
            _currentTimerSection = currentTimerSection;
            _stagedTimerService = stagedTimerService;
            _savedTimerService = savedTimerService;
            FillTimerSection();
        }

        private void FillTimerSection()
        {
            _currentTimerSection.Children.Clear();


            if (TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _currentTimerSection.Children.Add(new StagedTimerSectionView(advancedTimerModel.TimerSection));
            }
            else if (TimerModel is SimpleTimerModel simpleTimerModel)
            {
                _currentTimerSection.Children.Add(new StagedTimerSectionView(simpleTimerModel.Timer));
            }
        }

        public TimerModel TimerModel { get; set; }

        public void ResetTimer()
        {
            _savedTimerService.ResetTimer(TimerModel.Id);
        }

        public bool SaveTimer()
        {
            return _savedTimerService.SaveTimer(TimerModel.Id);
        }

        public void UnstageTimer()
        {
            _stagedTimerService.UnstageTimer(TimerModel);
        }
    }
}
