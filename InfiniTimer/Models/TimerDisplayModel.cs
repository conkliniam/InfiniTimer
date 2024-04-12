using InfiniTimer.Common;

namespace InfiniTimer.Models
{
    public class TimerDisplayModel : CommonBase
    {
        private Color _backColor;
        private Color _foreColor;
        private int _totalSeconds;
        private string _displayText;

        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                if (_backColor != value)
                {
                    _backColor = value;
                    RaisePropertyChanged(nameof(BackColor));
                }
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                if (_foreColor != value)
                {
                    _foreColor = value;
                    RaisePropertyChanged(nameof(ForeColor));
                }
            }
        }

        public int TotalSeconds
        {
            get
            {
                return _totalSeconds;
            }
            set
            {
                if (_totalSeconds != value)
                {
                    _totalSeconds = value;
                    RaisePropertyChanged(nameof(TotalSeconds));
                }
            }
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
                    RaisePropertyChanged(DisplayText);
                }
            }
        }
    }
}
