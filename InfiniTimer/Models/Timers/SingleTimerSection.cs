using InfiniTimer.Common;
using InfiniTimer.Enums;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : TimerSection
    {
        private string _displayText;
        private int _seconds;
        private TimerColor _color;

        public SingleTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public string DisplayText
        {
            get
            {
                return _displayText;
            }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;

                    RaisePropertyChanged(nameof(DisplayText));
                }
            }
        }

        public int Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                if (_seconds != value)
                {
                    _seconds = value;
                    RaisePropertyChanged(nameof(Seconds));
                }
            }
        }

        public TimerColor Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    RaisePropertyChanged(nameof(Color));
                }
            }
        }
    }
}
