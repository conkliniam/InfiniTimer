using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : ITimerSection
    {
        private ObservableCollection<ITimerSection> _timerSections;

        public SequentialTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public ObservableCollection<ITimerSection> TimerSections
        {
            get
            {
                return _timerSections;
            }
            set
            {
                if (_timerSections != value)
                {
                    var old = _timerSections;
                    _timerSections = value;

                    if (old != null)
                    {
                        old.CollectionChanged -= TimerSections_CollectionChanged;
                    }

                    if (_timerSections != null)
                    {
                        _timerSections.CollectionChanged += TimerSections_CollectionChanged;
                    }
                }
            }
        }

        private void TimerSections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= TimerSection_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += TimerSection_PropertyChanged;
                }
            }

            OnPropertyChanged(nameof(TimerSections));
        }

        protected void TimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("SequentialTimerSection.TimerSections." + e.PropertyName);
        }

        public int Depth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
