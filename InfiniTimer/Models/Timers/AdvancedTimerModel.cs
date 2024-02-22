namespace InfiniTimer.Models.Timers
{
    public class AdvancedTimerModel : TimerModel
    {
        private TimerSection _timerSection;
        private string _description;
        private bool _autoContinue;
        private bool _autoRepeat;

        public AdvancedTimerModel(bool ignoreChanges = true)
        {
            IgnoreChanges = ignoreChanges;
        }

        public TimerSection TimerSection
        {
            get
            {
                return _timerSection;
            }
            set
            {
                if (_timerSection != value)
                {
                    var old = _timerSection;
                    _timerSection = value;

                    if (old != null)
                    {
                        old.PropertyChanged -= TimerSection_PropertyChanged;
                    }

                    if (_timerSection != null)
                    {
                        _timerSection.PropertyChanged += TimerSection_PropertyChanged;
                    }

                    HandleChange();
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;

                    HandleChange();
                }
            }
        }

        public bool AutoContinue
        {
            get
            {
                return _autoContinue;
            }
            set
            {
                if (_autoContinue != value)
                {
                    _autoContinue = value;

                    HandleChange();
                }
            }
        }

        public bool AutoRepeat
        {
            get
            {
                return _autoRepeat;
            }
            set
            {
                if (_autoRepeat != value)
                {
                    _autoRepeat = value;

                    HandleChange();
                }
            }
        }
    }
}
