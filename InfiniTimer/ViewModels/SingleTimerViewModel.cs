using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class SingleTimerViewModel : CommonBase
    {
        #region Private Variables
        private Color _backColor;
        private Color _foreColor;
        private int _timerFont;
        private int _displayFont;
        private SingleTimerSection _singleTimerSection;
        private AppTheme _currentTheme;
        private string _timerDisplay;
        private string _timerSound;
        #endregion

        #region Constructors
        public SingleTimerViewModel(SingleTimerSection singleTimerSection, Application application)
        {
            SingleTimerSection = singleTimerSection;
            _currentTheme = application.RequestedTheme;
            application.RequestedThemeChanged += Application_RequestedThemeChanged;

            if (null != SingleTimerSection)
            {
                TimerDisplay = TimerHelper.GetTimerDisplay(SingleTimerSection.Seconds);
            }

            GetDisplayColors();
            GetFontSizes();
            GetTimerSound();

        }
        #endregion

        #region Properties
        public SingleTimerSection SingleTimerSection
        {
            get
            {
                return _singleTimerSection;
            }
            set
            {
                if (_singleTimerSection != value)
                {
                    _singleTimerSection = value;
                }
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            private set
            {
                if (_foreColor != value)
                {
                    _foreColor = value;
                    RaisePropertyChanged(nameof(ForeColor));
                }
            }
        }

        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            private set
            {
                if (_backColor != value)
                {
                    _backColor = value;
                    RaisePropertyChanged(nameof(BackColor));
                }
            }
        }

        public int DisplayFont
        {
            get
            {
                return _displayFont;
            }
            set
            {
                if (_displayFont != value)
                {
                    _displayFont = value;
                    RaisePropertyChanged(nameof(DisplayFont));
                }
            }
        }

        public int TimerFont
        {
            get
            {
                return _timerFont;
            }
            set
            {
                if (_timerFont != value)
                {
                    _timerFont = value;
                    RaisePropertyChanged(nameof(TimerFont));
                }
            }
        }

        public string TimerDisplay
        {
            get
            {
                return _timerDisplay;
            }
            set
            {
                if (_timerDisplay != value)
                {
                    _timerDisplay = value;
                    RaisePropertyChanged(nameof(TimerDisplay));
                }
            }
        }

        public string TimerSound
        {
            get
            {
                return _timerSound;
            }
            set
            {
                if (_timerSound != value)
                {
                    _timerSound = value;
                    RaisePropertyChanged(nameof(TimerSound));
                }
            }
        }
        #endregion

        #region Private Methods
        private void GetTimerSound()
        {
            var soundInfo = SoundHelper.SoundInfo[SingleTimerSection?.Sound ?? Enums.TimerSound.None];
            TimerSound = soundInfo.DisplayName;
        }

        private void GetFontSizes()
        {
            DisplayFont = 45 - 5 * SingleTimerSection.Depth;
            TimerFont = DisplayFont - 5;
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            _currentTheme = e.RequestedTheme;
            GetDisplayColors();
        }

        private void GetDisplayColors()
        {
            if (null != SingleTimerSection)
            {
                var colors = ColorHelper.TimerColors[SingleTimerSection.Color];
                BackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                ForeColor = _currentTheme == AppTheme.Light ? Colors.Black : Colors.White;
            }
        }
        #endregion
    }
}
