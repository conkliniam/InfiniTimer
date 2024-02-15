using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public abstract class TimerModel : INotifyPropertyChanged
    {
        private string _name;
        private bool _isDirty;

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
                        OnPropertyChanged(nameof(Name));
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
                        OnPropertyChanged(nameof(IsDirty));
                    }
                }
            }
        }

        public bool IgnoreChanges { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
