using InfiniTimer.Common;
using InfiniTimer.Common.Extensions;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Models
{
    public class NavigableTimers : CommonBase
    {
        #region Private Fields
        private readonly List<SingleTimerSection> _navigableTimers = new();
        private readonly TimerSection _timerSection;
        private int _currentIndex = 0;
        private SingleTimerSection _previous;
        private SingleTimerSection _current;
        private SingleTimerSection _next;
        #endregion

        #region Constructors
        public NavigableTimers(TimerSection timerSection)
        {
            _timerSection = timerSection;
            FillNavigableTimers();
        }
        #endregion

        #region Public Methods
        public void GoForward()
        {
            if (_currentIndex < _navigableTimers.Count)
            {
                _currentIndex++;
                SetProperties();
            }
        }

        public void GoBack()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                SetProperties();
            }
        }

        public void Restart()
        {
            _currentIndex = 0;
            SetProperties();
        }
        #endregion

        #region Public Properties
        public SingleTimerSection Previous
        {
            get
            {
                return _previous;
            }
            private set
            {
                if (_previous != value)
                {
                    _previous = value;
                    RaisePropertyChanged(nameof(Previous));
                }
            }
        }

        public SingleTimerSection Current
        {
            get
            {
                return _current;
            }
            private set
            {
                if (_current != value)
                {
                    _current = value;
                    RaisePropertyChanged(nameof(Current));
                }
            }
        }

        public SingleTimerSection Next
        {
            get
            {
                return _next;
            }
            private set
            {
                if (_next != value)
                {
                    _next = value;
                    RaisePropertyChanged(nameof(Next));
                }
            }
        }
        #endregion

        #region Private Methods
        private void FillNavigableTimers()
        {
            FillTimerSection(_timerSection);

            if (_navigableTimers.Any())
            {
                SetProperties();
            }
        }

        private void SetProperties()
        {
            Current = _navigableTimers.Count > _currentIndex ? _navigableTimers[_currentIndex] : null;
            Next = _navigableTimers.Count > _currentIndex + 1 ? _navigableTimers[_currentIndex + 1] : null;
            Previous = _currentIndex > 0 ? _navigableTimers[_currentIndex - 1] : null;
        }

        private void FillTimerSection(TimerSection timerSection)
        {
            if (timerSection is SingleTimerSection singleTimerSection)
            {
                _navigableTimers.Add(singleTimerSection.Copy());
            }
            else if (timerSection is AlternatingTimerSection alternatingTimerSection)
            {
                for (int i = 0; i < alternatingTimerSection.Cycles; i++)
                {
                    FillTimerSection(alternatingTimerSection.MainTimerSection);
                    FillTimerSection(alternatingTimerSection.AlternateTimerSection);
                }
            }
            else if (timerSection is SequentialTimerSection sequentialTimerSection)
            {
                if (sequentialTimerSection.TimerSections != null && sequentialTimerSection.TimerSections.Any())
                {
                    foreach (var listTimerSection in sequentialTimerSection.TimerSections)
                    {
                        FillTimerSection(listTimerSection);
                    }
                }
            }
        }
        #endregion
    }
}
