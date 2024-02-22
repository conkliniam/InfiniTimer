using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class AlternatingTimerSection : TimerSection
    {
        private TimerSection _mainTimerSection;
        private TimerSection _alternateTimerSection;
        private int _cycles;

        public AlternatingTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public TimerSection MainTimerSection
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

                    RaisePropertyChanged(nameof(MainTimerSection));
                }
            }
        }

        public TimerSection AlternateTimerSection
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

                    RaisePropertyChanged(nameof(AlternateTimerSection));
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
                    RaisePropertyChanged(nameof(Cycles));
                }
            }
        }

        protected void TimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("AlternatingTimerSection." + e.PropertyName);
        }
    }
}
