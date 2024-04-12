using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Views;

namespace InfiniTimer.ViewModels
{
    public class StagedTimerViewModel
    {
        private readonly VerticalStackLayout _currentTimerSection;
        private readonly IStagedTimerService _stagedTimerService;
        private readonly Page _page;
        private readonly ISavedTimerService _savedTimerService;

        public StagedTimerViewModel(TimerModel timerModel,
                                    VerticalStackLayout currentTimerSection,
                                    ISavedTimerService savedTimerService,
                                    IStagedTimerService stagedTimerService,
                                    Page page)
        {
            TimerModel = timerModel;
            _currentTimerSection = currentTimerSection;
            _stagedTimerService = stagedTimerService;
            _page = page;
            _savedTimerService = savedTimerService;
            FillTimerSection();
        }

        private void FillTimerSection()
        {
            _currentTimerSection.Children.Clear();


            if (TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _currentTimerSection
                    .Children
                    .Add(new StagedTimerSectionView(advancedTimerModel.TimerSection,
                                                    _page,
                                                    advancedTimerModel.AutoRepeat,
                                                    advancedTimerModel.AutoContinue));
            }
            else if (TimerModel is SimpleTimerModel simpleTimerModel)
            {
                _currentTimerSection
                    .Children
                    .Add(new StagedTimerSectionView(simpleTimerModel.Timer, _page));
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
