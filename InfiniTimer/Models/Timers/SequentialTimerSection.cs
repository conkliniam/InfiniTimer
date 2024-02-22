using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public class SequentialTimerSection : TimerSection
    {
        private ObservableCollection<TimerSection> _timerSections;

        public SequentialTimerSection(int depth = 0)
        {
            Depth = depth;
        }

        public ObservableCollection<TimerSection> TimerSections
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

            RaisePropertyChanged(nameof(TimerSections));
        }

        protected void TimerSection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("SequentialTimerSection.TimerSections." + e.PropertyName);
        }
    }
}
