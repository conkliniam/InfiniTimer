using InfiniTimer.Common;

namespace InfiniTimer.Models.Timers
{
    public class TimerModel : CommonBase
    {
        private bool _isDirty = true;
        private bool _isSelected;
        private bool _isStaged;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool AutoContinue { get; set; }

        public bool AutoRepeat { get; set; }

        public TimerListSection Timers { get; set; } = new TimerListSection();

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;

                    if (!IgnoreChanges)
                    {
                        RaisePropertyChanged(nameof(IsDirty));
                    }
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    if (!IgnoreChanges)
                    {
                        RaisePropertyChanged(nameof(IsSelected));
                    }
                }
            }
        }

        public bool IsStaged
        {
            get
            {
                return _isStaged;
            }
            set
            {
                if (_isStaged != value)
                {
                    _isStaged = value;

                    HandleChange(nameof(IsStaged));
                }
            }
        }

        public bool IgnoreChanges { get; set; }

        protected void HandleChange(string propName)
        {
            if (!IgnoreChanges)
            {
                if (!IsDirty)
                {
                    IsDirty = true;
                }

                RaisePropertyChanged(propName);
            }
        }
    }
}
