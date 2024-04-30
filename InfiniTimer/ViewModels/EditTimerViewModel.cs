using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using System.Windows.Input;

namespace InfiniTimer.ViewModels
{
    public class EditTimerViewModel
    {
        #region Private Fields
        private readonly EditTimerContent _timerContent;
        private readonly ISavedTimerService _savedTimerService;
        private readonly IStagedTimerService _stagedTimerService;
        #endregion

        #region Constructors
        public EditTimerViewModel(EditTimerContent timerContent,
                                  TimerModel timerModel,
                                  ISavedTimerService savedTimerService,
                                  IStagedTimerService stagedTimerService,
                                  string title,
                                  INavigation navigation)
        {
            _timerContent = timerContent;
            _savedTimerService = savedTimerService;
            _stagedTimerService = stagedTimerService;
            Title = title;
            if (timerModel == null)
            {
                timerModel = new TimerModel();
                _savedTimerService.AddSessionTimer(timerModel);
            }

            TimerModel = timerModel;
            BackCommand = new Command(async () =>
            {
                SendDoneEditingMessage();
                await navigation.PopAsync();
            });

            NextColor = ColorHelper.ThemeColors[ColorHelper.Primary];
            _timerContent.TimerSection = TimerModel.Timers;
            _timerContent.Depth = 1;
        }
        #endregion

        #region Public Properties
        public TimerModel TimerModel { get; set; }
        public string Title { get; }
        public Color NextColor { get; set; }
        #endregion

        #region Public Methods
        public void HandleCancel()
        {
            _savedTimerService.ResetTimer(TimerModel.Id);
        }

        public bool HandleSave()
        {
            var success = _savedTimerService.SaveTimer(TimerModel.Id);
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(TimerModel));
            return success;
        }

        public void HandleStage()
        {
            _stagedTimerService.StageTimer(TimerModel);
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(TimerModel));
        }

        public void SendDoneEditingMessage()
        {
            TimerModel.IsDirty = true;
            WeakReferenceMessenger.Default.Send(new TimerDoneEditingMessage(TimerModel));
        }

        public ICommand BackCommand { get; set; }
        #endregion
    }
}
