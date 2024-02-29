using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.ViewModels;

namespace InfiniTimer
{
    public partial class MainPage : ContentPage
    {
        private readonly IStagedTimerService _stagedTimerService;
        private readonly ISavedTimerService _savedTimerService;

        public MainPage(IStagedTimerService stagedTimerService, ISavedTimerService savedTimerService)
        {
            InitializeComponent();
            BindingContext = new StagedTimersViewModel(stagedTimerService, infiniTimerLogo);
            _stagedTimerService = stagedTimerService;
            _savedTimerService = savedTimerService;
        }

        private void OnAddTimerClicked(object sender, EventArgs e)
        {
            TimerModel newTimerModel = new SimpleTimerModel();
            _savedTimerService.AddSessionTimer(newTimerModel);
            Navigation.PushAsync(new EditTimerPage(newTimerModel,
                                                   _savedTimerService,
                                                   _stagedTimerService,
                                                   "Add Timer",
                                                   false));
        }

        private void UnstageIconClicked(object sender, EventArgs e)
        {

        }

        private void SaveIconClicked(object sender, EventArgs e)
        {

        }

        private void ResetIconClicked(object sender, EventArgs e)
        {

        }
    }
}