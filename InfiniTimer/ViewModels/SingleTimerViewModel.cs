using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region Constructors
        public SingleTimerViewModel(SingleTimerSection singleTimerSection, Application application)
        {
            SingleTimerSection = singleTimerSection;
            _currentTheme = application.RequestedTheme;
            application.RequestedThemeChanged += Application_RequestedThemeChanged;

            if (null != SingleTimerSection)
            {
                TimerDisplay = GetTimerDisplay(SingleTimerSection.Seconds);
            }

            GetDisplayColors();
            GetFontSizes();

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
                    //var old = _singleTimerSection;
                    _singleTimerSection = value;

                    //if (old != null)
                    //{
                    //    old.PropertyChanged -= SingleTimerSection_PropertyChanged;
                    //}

                    //if (_singleTimerSection != null)
                    //{
                    //    _singleTimerSection.PropertyChanged += SingleTimerSection_PropertyChanged;
                    //}
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
        #endregion

        #region Private Methods
        private void SingleTimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SingleTimerSection.Seconds))
            {
                TimerDisplay = GetTimerDisplay(SingleTimerSection.Seconds);
            }
            else if (e.PropertyName == nameof(SingleTimerSection.Color))
            {
                GetDisplayColors();
            }
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

        private static string GetTimerDisplay(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = totalSeconds % 3600 / 60;
            int seconds = totalSeconds % 60;

            if (hours > 0)
                return $"{Convert.ToString(hours)}:{Convert.ToString(minutes).PadLeft(2, '0')}:{Convert.ToString(seconds).PadLeft(2, '0')}";
            if (minutes > 0)
                return $"{Convert.ToString(minutes).PadLeft(2, '0')}:{Convert.ToString(seconds).PadLeft(2, '0')}";
            return $"00:{Convert.ToString(seconds).PadLeft(2, '0')}";
        }
        #endregion
    }
}
