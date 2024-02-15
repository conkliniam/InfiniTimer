using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerSection : ITimerSection
    {
        private ITimerSection _mainTimerSection;
        private ITimerSection _alternateTimerSection;
        private int _cycles;

        public AlternatingTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public ITimerSection MainTimerSection
        {
            get
            {
                return _mainTimerSection;
            }
            set
            {
                if (_mainTimerSection != value)
                {
                    var old = _mainTimerSection;
                    _mainTimerSection = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= TimerSection_PropertyChanged;
                    }

                    if (_mainTimerSection != null)
                    {
                        _mainTimerSection.PropertyChanged += TimerSection_PropertyChanged;
                    }

                    OnPropertyChanged(nameof(MainTimerSection));
                }
            }
        }

        public ITimerSection AlternateTimerSection
        {
            get
            {
                return _alternateTimerSection;
            }
            set
            {
                if (_alternateTimerSection != value)
                {
                    var old = _alternateTimerSection;
                    _alternateTimerSection = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= TimerSection_PropertyChanged;
                    }

                    if (_alternateTimerSection != null)
                    {
                        _alternateTimerSection.PropertyChanged += TimerSection_PropertyChanged;
                    }

                    OnPropertyChanged(nameof(AlternateTimerSection));
                }
            }
        }

        public int Cycles
        {
            get
            {
                return _cycles;
            }
            set
            {
                if (_cycles != value)
                {
                    _cycles = value;
                    OnPropertyChanged(nameof(Cycles));
                }
            }
        }

        public int Depth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void TimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("AlternatingTimerSection." + e.PropertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
