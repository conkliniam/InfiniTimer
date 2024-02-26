using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services.Messages;
using System.Collections.ObjectModel;

namespace InfiniTimer.Services
{
    public class StagedTimerService : IStagedTimerService
    {
        #region Private Fields
        private readonly ISavedTimerService _savedTimerService;
        private ObservableCollection<TimerModel> _stagedTimers;
        private const bool Staged = true;
        private const bool Unstaged = false;
        #endregion

        #region Constructors
        public StagedTimerService(ISavedTimerService savedTimerService)
        {
            _savedTimerService = savedTimerService;
            InitializeStagedTimers();
        }
        #endregion

        #region IStagedTimerService Methods
        public void StageTimer(TimerModel timer, bool sendMessage = true)
        {
            if (!_stagedTimers.Contains(timer))
            {
                _stagedTimers.Add(timer);
                timer.IsStaged = true;

                if (sendMessage)
                {
                    WeakReferenceMessenger.Default.Send(new StagedTimersChangedMessage(Staged));
                }
            }
        }

        public void StageTimers(List<TimerModel> timers)
        {
            if (timers.Any())
            {
                foreach (TimerModel timer in timers)
                {
                    StageTimer(timer, false);
                }

                WeakReferenceMessenger.Default.Send(new StagedTimersChangedMessage(Staged));
            }
        }

        public void UnstageTimers(List<TimerModel> timers)
        {
            if (timers.Any())
            {
                foreach (TimerModel timer in timers)
                {
                    UnstageTimer(timer, false);
                }

                WeakReferenceMessenger.Default.Send(new StagedTimersChangedMessage(Unstaged));
            }
        }

        public ObservableCollection<TimerModel> GetStagedTimers()
        {
            return _stagedTimers;
        }

        public void UnstageAllTimers()
        {
            if (_stagedTimers.Any())
            {
                foreach (TimerModel timer in _stagedTimers)
                {
                    timer.IsStaged = false;
                }

                _stagedTimers.Clear();
                WeakReferenceMessenger.Default.Send(new StagedTimersChangedMessage(Unstaged));
            }
        }

        public void UnstageTimer(TimerModel timer, bool sendMessage = true)
        {
            if (_stagedTimers.Contains(timer))
            {
                timer.IsStaged = false;
                _stagedTimers.Remove(timer);

                if (sendMessage)
                {
                    WeakReferenceMessenger.Default.Send(new StagedTimersChangedMessage(Unstaged));
                }
            }
        }
        #endregion

        #region Private Methods
        private void InitializeStagedTimers()
        {
            _stagedTimers = new ObservableCollection<TimerModel>
                (_savedTimerService
                .GetSessionTimers()
                .Where(timerModel => timerModel.IsStaged)
                .ToList());
        }
        #endregion
    }
}
