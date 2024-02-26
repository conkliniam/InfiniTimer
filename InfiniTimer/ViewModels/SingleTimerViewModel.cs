using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using System;
using System.Collections.Generic;
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
        #endregion

        #region Constructors
        public SingleTimerViewModel(SingleTimerSection singleTimerSection, Application application)
        {
            SingleTimerSection = singleTimerSection;
            application.RequestedThemeChanged += Application_RequestedThemeChanged;

            if (null != SingleTimerSection)
            {
                TimerDisplay = GetTimerDisplay(SingleTimerSection.Seconds);
            }

            GetDisplayColors(application.RequestedTheme);
            GetFontSizes();

        }
        #endregion

        #region Properties
        public SingleTimerSection SingleTimerSection { get; }

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

        public string TimerDisplay { get; private set; }
        #endregion

        #region Private Methods
        private void GetFontSizes()
        {
            // Depth 5: Display Font = 20
            // Depth 5 Timer Font = 15
            DisplayFont = 45 - 5 * SingleTimerSection.Depth;
            TimerFont = DisplayFont - 5;
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            GetDisplayColors(e.RequestedTheme);
        }



        private void GetDisplayColors(AppTheme appTheme)
        {
            if (null != SingleTimerSection)
            {
                var colors = ColorHelper.TimerColors[SingleTimerSection.Color];
                BackColor = appTheme == AppTheme.Light ? colors.Light : colors.Dark;
                ForeColor = appTheme == AppTheme.Light ? Colors.Black : Colors.White;
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
