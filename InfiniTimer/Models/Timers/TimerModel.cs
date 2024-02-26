using InfiniTimer.Common;
using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public abstract class TimerModel : CommonBase
    {
        private string _name;
        private bool _isDirty;
        private bool _isSelected;
        private bool _isStaged;

        public Guid Id { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {

                    _name = value;

                    if (!IgnoreChanges)
                    {
                        HandleChange();
                        RaisePropertyChanged(nameof(Name));
                    }
                }
            }
        }
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

                    if (!IgnoreChanges)
                    {
                        HandleChange();
                        RaisePropertyChanged(nameof(IsStaged));
                    }
                }
            }
        }

        public bool IgnoreChanges { get; set; }

        protected void HandleChange()
        {
            if (!IgnoreChanges && !IsDirty)
            {
                IsDirty = true;
            }
        }

        protected void TimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HandleChange();
        }
    }
}
