using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class StagedTimerSectionViewModel : CommonBase
    {
        #region Private Fields
        private AppTheme _currentTheme;
        private Color _prevBackColor;
        private Color _prevForeColor;
        private Color _currentBackColor;
        private Color _currentForeColor;
        private Color _nextBackColor;
        private Color _nextForeColor;
        private bool _prevVisible;
        private bool _nextVisible;
        private decimal _secondsRemaining;
        private bool _timerRunning;
        private Animation _timerAnimation;
        private readonly CircularProgressBar _currentTimer;
        private const string TimerAnimation = "TimerAnimation";
        #endregion

        #region Constructors
        public StagedTimerSectionViewModel(TimerSection timerSection, Application application, CircularProgressBar currentTimer)
        {
            _currentTheme = application.RequestedTheme;
            NavigableTimers = new NavigableTimers(timerSection);
            InitializeCurrentTimerValues();
            _currentTimer = currentTimer;
        }
        #endregion

        #region Public Properties
        public Color PrevBackColor
        {
            get
            {
                return _prevBackColor;
            }
            set
            {
                if (value != _prevBackColor)
                {
                    _prevBackColor = value;
                    RaisePropertyChanged(nameof(PrevBackColor));
                }
            }
        }

        public Color PrevForeColor
        {
            get
            {
                return _prevForeColor;
            }
            set
            {
                if (_prevForeColor != value)
                {
                    _prevForeColor = value;
                    RaisePropertyChanged(nameof(PrevForeColor));
                }
            }
        }

        public Color CurrentBackColor
        {
            get
            {
                return _currentBackColor;
            }
            set
            {
                if (value != _currentBackColor)
                {
                    _currentBackColor = value;
                    RaisePropertyChanged(nameof(CurrentBackColor));
                }
            }
        }

        public Color CurrentForeColor
        {
            get
            {
                return _currentForeColor;
            }
            set
            {
                if (_currentForeColor != value)
                {
                    _currentForeColor = value;
                    RaisePropertyChanged(nameof(CurrentForeColor));
                }
            }
        }

        public Color NextBackColor
        {
            get
            {
                return _nextBackColor;
            }
            set
            {
                if (value != _nextBackColor)
                {
                    _nextBackColor = value;
                    RaisePropertyChanged(nameof(NextBackColor));
                }
            }
        }

        public Color NextForeColor
        {
            get
            {
                return _nextForeColor;
            }
            set
            {
                if (_nextForeColor != value)
                {
                    _nextForeColor = value;
                    RaisePropertyChanged(nameof(NextForeColor));
                }
            }
        }

        public NavigableTimers NavigableTimers { get; set; }

        public decimal SecondsRemaining
        {
            get
            {
                return _secondsRemaining;
            }
            private set
            {
                if (_secondsRemaining != value)
                {
                    _secondsRemaining = value;
                    RaisePropertyChanged(nameof(SecondsRemaining));
                }
            }
        }

        public bool PrevVisible
        {
            get
            {
                return _prevVisible;
            }
            private set
            {
                if (_prevVisible != value)
                {
                    _prevVisible = value;
                    RaisePropertyChanged(nameof(PrevVisible));
                }
            }
        }

        public bool NextVisible
        {
            get
            {
                return _nextVisible;
            }
            private set
            {
                if (_nextVisible != value)
                {
                    _nextVisible = value;
                    RaisePropertyChanged(nameof(NextVisible));
                }
            }
        }

        public bool TimerRunning
        {
            get
            {
                return _timerRunning;
            }
            set
            {
                if (_timerRunning != value)
                {
                    _timerRunning = value;
                    RaisePropertyChanged(nameof(TimerRunning));
                }
            }
        }
        #endregion

        #region Public Methods
        public void GoBack()
        {
            if (TimerRunning)
            {
                _currentTimer.AbortAnimation(TimerAnimation);
                TimerRunning = false;
            }
            NavigableTimers.GoBack();
            InitializeCurrentTimerValues();
        }

        public void GoForward()
        {
            if (TimerRunning)
            {
                _currentTimer.AbortAnimation(TimerAnimation);
                TimerRunning = false;
            }
            NavigableTimers.GoForward();
            InitializeCurrentTimerValues();
        }
        #endregion

        #region Private Methods
        private void InitializeCurrentTimerValues()
        {
            SecondsRemaining = NavigableTimers.Current.Seconds;
            NextVisible = NavigableTimers.Next != null;
            PrevVisible = NavigableTimers.Previous != null;
            GetDisplayColors();
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            _currentTheme = e.RequestedTheme;
            GetDisplayColors();
        }

        private void GetDisplayColors()
        {
            if (null != NavigableTimers.Current)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Current.Color];
                CurrentBackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                CurrentForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
            }

            if (null != NavigableTimers.Previous)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Previous.Color];
                PrevBackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                PrevForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
            }

            if (null != NavigableTimers.Next)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Next.Color];
                NextBackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                NextForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
            }
        }

        public void StartTimer()
        {
            TimerRunning = true;
            _timerAnimation = new Animation(v => SecondsRemaining = (decimal)v, Convert.ToDouble(SecondsRemaining), 0);
            _timerAnimation.Commit(_currentTimer, TimerAnimation, 16, (uint)(SecondsRemaining * 1000), Easing.Linear);
        }

        public void PauseTimer()
        {
            TimerRunning = false;
            _currentTimer.AbortAnimation(TimerAnimation);
        }

        public void CancelTimer()
        {
            if (TimerRunning)
            {
                TimerRunning = false;
                _currentTimer.AbortAnimation(TimerAnimation);
            }

            NavigableTimers.Restart();
            InitializeCurrentTimerValues();
        }
        #endregion
    }
}
