using InfiniTimer.Common;
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
            BindingContext = new StagedTimersViewModel(stagedTimerService, savedTimerService, infiniTimerLogo);
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

        private async void UnstageIconClicked(object sender, EventArgs e)
        {
            try
            {
                TimerModel timerModel = (TimerModel)((ImageButton)sender).CommandParameter;

                if (null != timerModel)
                {
                    ((StagedTimersViewModel)BindingContext).UnstageTimer(timerModel);
                    await MessageHelper.ShowSuccessMessage("Timer Unstaged");
                }
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private async void SaveIconClicked(object sender, EventArgs e)
        {
            try
            {
                TimerModel timerModel = (TimerModel)((ImageButton)sender).CommandParameter;

                if (null != timerModel)
                {
                    var success = ((StagedTimersViewModel)BindingContext).SaveTimer(timerModel);

                    if (success)
                    {
                        await MessageHelper.ShowSuccessMessage("Timer Saved");
                    }
                    else
                    {
                        await MessageHelper.ShowFailureMessage("Save Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private async void ResetIconClicked(object sender, EventArgs e)
        {
            try
            {
                TimerModel timerModel = (TimerModel)((ImageButton)sender).CommandParameter;

                if (null != timerModel)
                {
                    ((StagedTimersViewModel)BindingContext).ResetTimer(timerModel);
                    await MessageHelper.ShowSuccessMessage("Timer Reset");
                }
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private async void UnstageAllClicked(object sender, EventArgs e)
        {
            try
            {
                ((StagedTimersViewModel)BindingContext).UnstageAll();
                await MessageHelper.ShowSuccessMessage("Timers Unstaged");
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }
    }
}