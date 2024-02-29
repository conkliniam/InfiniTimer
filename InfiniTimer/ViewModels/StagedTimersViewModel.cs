using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class StagedTimersViewModel : CommonBase
    {
        #region Private Fields
        private readonly IStagedTimerService _stagedTimerService;
        private readonly ISavedTimerService _savedTimerService;
        private readonly Image _logoImage;
        private Animation _rotateAnimation;
        private GridLength _topHeight;
        private GridLength _midHeight;
        private bool _welcomeVisible;
        #endregion

        #region Constructor
        public StagedTimersViewModel(IStagedTimerService stagedTimerService, ISavedTimerService savedTimerService, Image logoImage)
        {
            _stagedTimerService = stagedTimerService;
            _savedTimerService = savedTimerService;
            _logoImage = logoImage;
            StagedTimers = _stagedTimerService.GetStagedTimers();
            InitializeContent();
            RegisterForMessages();
        }
        #endregion

        #region Public Properties
        public ObservableCollection<TimerModel> StagedTimers { get; set; }

        public GridLength TopHeight
        {
            get
            {
                return _topHeight;
            }
            set
            {
                _topHeight = value;
                RaisePropertyChanged(nameof(TopHeight));
            }
        }

        public GridLength MidHeight
        {
            get
            {
                return _midHeight;
            }
            set
            {
                _midHeight = value;
                RaisePropertyChanged(nameof(MidHeight));
            }
        }

        public bool WelcomeVisible
        {
            get
            {
                return _welcomeVisible;
            }
            set
            {
                if (_welcomeVisible != value)
                {
                    _welcomeVisible = value;
                    RaisePropertyChanged(nameof(WelcomeVisible));
                }
            }
        }
        #endregion

        #region Public Methods
        public void UnstageAll()
        {
            _stagedTimerService.UnstageAllTimers();
        }

        public void ResetTimer(TimerModel timerModel)
        {
            _savedTimerService.ResetTimer(timerModel.Id);
        }

        public bool SaveTimer(TimerModel timerModel)
        {
            return _savedTimerService.SaveTimer(timerModel.Id);
        }

        public void UnstageTimer(TimerModel timerModel)
        {
            _stagedTimerService.UnstageTimer(timerModel);
        }
        #endregion

        #region Private Methods
        private void RegisterForMessages()
        {
            WeakReferenceMessenger.Default.Register<StagedTimersChangedMessage>(this, (r, m) =>
            {
                OnStagedTimersChange(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerDoneEditingMessage>(this, (r, m) =>
            {
                OnTimerDoneEditing(m.Value);
            });
        }

        private void OnTimerDoneEditing(TimerModel timer)
        {
            if (timer.IsStaged)
            {
                timer.RaisePropertyChanged(nameof(TimerModel.Name));
            }
        }

        private void OnStagedTimersChange(bool staged)
        {
            if (staged && WelcomeVisible)
            {
                DisplayTimers();
            }
            else if (!staged && !WelcomeVisible)
            {
                if (!StagedTimers.Any())
                {
                    DisplayWelcome();
                }
            }
        }

        private void InitializeContent()
        {
            _rotateAnimation = new Animation(v => _logoImage.Rotation = v, 0, 360);

            if (StagedTimers.Any())
            {
                DisplayTimers();
            }
            else
            {
                DisplayWelcome();
            }
        }

        private void DisplayWelcome()
        {
            TopHeight = new GridLength(0);
            MidHeight = new GridLength(11, GridUnitType.Star);
            WelcomeVisible = true;
            _rotateAnimation.Commit(_logoImage, "RotateAnimation", 16, 4000, null, null, () => !StagedTimers.Any());
        }

        private void DisplayTimers()
        {
            TopHeight = new GridLength(1, GridUnitType.Star);
            MidHeight = new GridLength(10, GridUnitType.Star);
            WelcomeVisible = false;
        }
        #endregion
    }
}
