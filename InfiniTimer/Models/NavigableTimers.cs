using InfiniTimer.Common;
using InfiniTimer.Common.Extensions;
using InfiniTimer.Models.Timers;
using System.Net.WebSockets;

namespace InfiniTimer.Models
{
    public class NavigableTimers : CommonBase
    {
        #region Private Fields
        private readonly List<SingleTimerSection> _navigableTimers = new();
        private readonly List<int> _indices = new();
        private SingleTimerSection _previous;
        private SingleTimerSection _current;
        private SingleTimerSection _next;
        private int _currentIndex;
        #endregion

        #region Constructors
        public NavigableTimers(TimerSection timerSection)
        {
            FillNavigableTimers(timerSection);
        }
        #endregion

        #region Public Methods
        public void GoForward()
        {
            if (_currentIndex < _indices.Count - 1)
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
        private void FillNavigableTimers(TimerSection timerSection)
        {
            FillTimerSection(timerSection);

            if (_navigableTimers.Count != 0)
            {
                SetProperties();
            }
        }

        private void SetProperties()
        {
            var index = CheckBounds(_indices.Count, _currentIndex) ? _indices[_currentIndex] : -1;
            var nextIndex = CheckBounds(_indices.Count, _currentIndex + 1) ? _indices[_currentIndex + 1] : -1;
            var previousIndex = CheckBounds(_indices.Count, _currentIndex - 1) ? _indices[_currentIndex - 1] : -1;

            Current = CheckBounds(_navigableTimers.Count, index) ? _navigableTimers[index] : null;
            Next = CheckBounds(_navigableTimers.Count, nextIndex) ? _navigableTimers[nextIndex] : null;
            Previous = CheckBounds(_navigableTimers.Count, previousIndex) ? _navigableTimers[previousIndex] : null;
        }

        private void FillTimerSection(TimerSection timerSection)
        {
            if (timerSection is SingleTimerSection singleTimerSection)
            {
                _navigableTimers.Add(singleTimerSection.Copy());
                _indices.Add(_navigableTimers.Count - 1);
            }
            else if (timerSection is TimerListSection timerListSection)
            {
                if (timerListSection.TimerSections != null && timerListSection.TimerSections.Count != 0)
                {
                    var startIndex = _indices.Count;

                    foreach (var listTimerSection in timerListSection.TimerSections)
                    {
                        FillTimerSection(listTimerSection);
                    }

                    var timerCount = _indices.Count - startIndex;

                    if (timerCount > 0 && timerListSection.Cycles > 1)
                    {
                        for (int cycle = 2; cycle <= timerListSection.Cycles; cycle++)
                        {
                            _indices.AddRange(_indices.GetRange(startIndex, timerCount));
                        }
                    }
                }
            }
        }

        private static bool CheckBounds(int length, int index)
        {
            return index >= 0 && index < length;
        }
        #endregion
    }
}
