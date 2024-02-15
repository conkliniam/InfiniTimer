using InfiniTimer.Enums;
using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class SingleTimerSection : ITimerSection
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
                    OnPropertyChanged(nameof(DisplayText));
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
                    OnPropertyChanged(nameof(Seconds));
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
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        public int Depth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
