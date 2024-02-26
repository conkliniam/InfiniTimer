using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Common.Extensions;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using InfiniTimer.Views;

namespace InfiniTimer.ViewModels
{
    public class TimerViewModel : CommonBase
    {
        #region Private Fields
        private readonly StackLayout _timerContent;
        private readonly ISavedTimerService _savedTimerService;
        private readonly IStagedTimerService _stagedTimerService;
        private TimerModel _timerModel;
        #endregion

        #region Constructors
        public TimerViewModel(TimerModel timerModel,
                              StackLayout timerContent,
                              ISavedTimerService savedTimerService,
                              IStagedTimerService stagedTimerService)
        {
            TimerModel = timerModel;
            _timerContent = timerContent;
            _savedTimerService = savedTimerService;
            _stagedTimerService = stagedTimerService;
            FillTimerContent();
            RegisterForMessages();
        }
        #endregion

        #region Public Properties
        public TimerModel TimerModel
        {
            get
            {
                return _timerModel;
            }
            set
            {
                if (_timerModel != value)
                {
                    _timerModel = value;

                    RaisePropertyChanged(nameof(TimerModel));
                }
            }
        }
        #endregion

        #region Public Methods
        public void HandleStageTimer()
        {
            _stagedTimerService.StageTimer(TimerModel);
        }

        public void HandleUnstageTimer()
        {
            _stagedTimerService.UnstageTimer(TimerModel);
        }

        public void Copy()
        {
            var copy = TimerModel.Copy();
            copy.Name = $"{copy.Name} Copy";
            copy.Id = Guid.NewGuid();
            _savedTimerService.AddSessionTimer(copy);
            TimerModel = copy;
        }

        public bool HandleSaveTimer()
        {
            return _savedTimerService.SaveTimer(TimerModel.Id);
        }

        public void HandleResetTimer()
        {
            _savedTimerService.ResetTimer(TimerModel.Id);
        }

        public bool HandleDeleteTimer()
        {
            return _savedTimerService.DeleteTimer(TimerModel.Id);
        }
        #endregion

        #region Private Methods
        private void FillTimerContent()
        {
            _timerContent.Children.Clear();

            if (TimerModel is AdvancedTimerModel advancedTimerModel)
            {
                _timerContent.Children.Add(new AdvancedTimerView(advancedTimerModel));
            }
            else if (TimerModel is SimpleTimerModel simpleTimerModel)
            {
                _timerContent.Children.Add(new ViewTimerContent { TimerSection = simpleTimerModel.Timer });
            }
        }

        private void RegisterForMessages()
        {
            WeakReferenceMessenger.Default.Register<TimerReplacedMessage>(this, (r, m) =>
            {
                OnTimerReplaced(m.Value);
            });
        }

        private void OnTimerReplaced(TimerModel value)
        {
            if (value.Id == TimerModel.Id)
            {
                TimerModel = value;
            }
        }

        public EditTimerPage GetEditPage()
        {
            return new EditTimerPage(TimerModel, _savedTimerService, _stagedTimerService);
        }
        #endregion
    }
}
