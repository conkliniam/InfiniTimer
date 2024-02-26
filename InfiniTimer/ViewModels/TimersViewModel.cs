using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class TimersViewModel : CommonBase
    {
        #region Private Fields
        private readonly ISavedTimerService _savedTimerService;
        private readonly IStagedTimerService _stagedTimerService;
        private bool _anyTimers;
        private bool _anySelected;
        private bool _anyUnselected;
        private bool _anyStagedSelected;
        private bool _anyUnstagedSelected;
        #endregion

        #region Constructors
        public TimersViewModel(ISavedTimerService savedTimerService, IStagedTimerService stagedTimerService)
        {
            _savedTimerService = savedTimerService;
            _stagedTimerService = stagedTimerService;
            TimerModels = new ObservableCollection<TimerModel>(_savedTimerService.GetSessionTimers());
            AnyTimers = TimerModels.Any();
            CalculateSelected();
            AddPropertyChangedChecks();
            RegisterForMessages();
        }
        #endregion

        #region Public Properties
        public ObservableCollection<TimerModel> TimerModels { get; set; }


        public bool AnyTimers
        {
            get
            {
                return _anyTimers;
            }
            set
            {
                if (_anyTimers != value)
                {
                    _anyTimers = value;
                    RaisePropertyChanged(nameof(AnyTimers));
                }
            }
        }


        public bool AnySelected
        {
            get
            {
                return _anySelected;
            }
            set
            {
                if (_anySelected != value)
                {
                    _anySelected = value;
                    RaisePropertyChanged(nameof(AnySelected));
                }
            }
        }

        public bool AnyUnselected
        {
            get
            {
                return _anyUnselected;
            }
            set
            {
                if (_anyUnselected != value)
                {
                    _anyUnselected = value;
                    RaisePropertyChanged(nameof(AnyUnselected));
                }
            }
        }

        public bool AnyUnstagedSelected
        {
            get
            {
                return _anyUnstagedSelected;
            }
            set
            {
                if (_anyUnstagedSelected != value)
                {
                    _anyUnstagedSelected = value;
                    RaisePropertyChanged(nameof(AnyUnstagedSelected));
                }
            }
        }

        public bool AnyStagedSelected
        {
            get
            {
                return _anyStagedSelected;
            }
            set
            {
                if (_anyStagedSelected != value)
                {
                    _anyStagedSelected = value;
                    RaisePropertyChanged(nameof(AnyStagedSelected));
                }
            }
        }
        #endregion

        #region Public Methods
        public bool HandleSaveTimer(TimerModel timerModel)
        {
            var success = false;

            if (null != timerModel)
            {
                success = _savedTimerService.SaveTimer(timerModel.Id);
            }

            return success;
        }

        public bool HandleResetTimer(TimerModel timerModel)
        {
            bool success = false;

            if (null != timerModel)
            {
                _savedTimerService.ResetTimer(timerModel.Id);
                success = true;
            }

            return success;
        }

        public bool HandleDeleteTimer(TimerModel timerModel)
        {
            bool success = false;

            if (null != timerModel)
            {
                success = _savedTimerService.DeleteTimer(timerModel.Id);
                TimerModels.Remove(timerModel);
                CalculateSelected();
            }

            AnyTimers = TimerModels.Any();

            return success;
        }

        public bool HandleDeleteSelected()
        {
            var selectedIds = TimerModels
                .Where(timerModel => timerModel.IsSelected)
                .Select(timerModel => timerModel.Id)
                .ToList();

            var success = _savedTimerService.DeleteTimers(selectedIds);
            AnySelected = false;
            AnyUnselected = AnyTimers = TimerModels.Any();
            return success;
        }

        public void HandleStageSelected()
        {
            var selectedTimers = TimerModels.Where(timerModel => timerModel.IsSelected && !timerModel.IsStaged).ToList();
            _stagedTimerService.StageTimers(selectedTimers);
        }

        public void HandleUnstageSelected()
        {
            var selectedTimers = TimerModels.Where(timerModel => timerModel.IsSelected && timerModel.IsStaged).ToList();
            _stagedTimerService.UnstageTimers(selectedTimers);
        }

        public bool HandleDeleteAll()
        {
            return _savedTimerService.DeleteAllTimers();
        }

        public bool HandleAddDefault()
        {
            List<TimerModel> newTimers = TimerHelper.GetDefaultTimers();

            foreach (TimerModel timerModel in newTimers)
            {
                timerModel.PropertyChanged += Timer_PropertyChanged;
                TimerModels.Add(timerModel);
                _savedTimerService.AddSessionTimer(timerModel);
            }

            CalculateSelected();
            AnyTimers = TimerModels.Any();

            return _savedTimerService.SaveTimers(newTimers.Select(timer => timer.Id).ToList());
        }

        public void SelectAll()
        {
            foreach (var timerModel in TimerModels)
            {
                if (!timerModel.IsSelected)
                {
                    timerModel.PropertyChanged -= Timer_PropertyChanged;
                    timerModel.IsSelected = true;
                    timerModel.PropertyChanged += Timer_PropertyChanged;
                }
            }

            CalculateSelected();
        }

        public void UnselectAll()
        {
            foreach (var timerModel in TimerModels)
            {
                if (timerModel.IsSelected)
                {
                    timerModel.PropertyChanged -= Timer_PropertyChanged;
                    timerModel.IsSelected = false;
                    timerModel.PropertyChanged += Timer_PropertyChanged;
                }
            }

            CalculateSelected();
        }
        #endregion

        #region Private Methods
        private void CalculateSelected()
        {
            var totalSelected = TimerModels.Where(timer => timer.IsSelected).Count();
            var selectedStaged = TimerModels.Where(timer => timer.IsSelected && timer.IsStaged).Count();
            var totalTimers = TimerModels.Count;
            AnySelected = totalSelected > 0;
            AnyUnselected = totalSelected < totalTimers;
            AnyStagedSelected = selectedStaged > 0;
            AnyUnstagedSelected = selectedStaged < totalSelected;
        }

        private void AddPropertyChangedChecks()
        {
            foreach (TimerModel timer in TimerModels)
            {
                timer.PropertyChanged += Timer_PropertyChanged;
            }
        }

        private void Timer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TimerModel.IsSelected))
            {
                CalculateSelected();
            }
        }

        private void RegisterForMessages()
        {
            WeakReferenceMessenger.Default.Register<TimerRemovedMessage>(this, (r, m) =>
            {
                OnTimerRemoved(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerReplacedMessage>(this, (r, m) =>
            {
                OnTimerReplaced(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerAddedMessage>(this, (r, m) =>
            {
                OnTimerAdded(m.Value);
            });

            WeakReferenceMessenger.Default.Register<StagedTimersChangedMessage>(this, (r, m) =>
            {
                CalculateSelected();
            });
        }

        private void OnTimerAdded(TimerModel savedTimer)
        {
            if (savedTimer != null)
            {
                if (!TimerModels.Contains(savedTimer))
                {
                    savedTimer.PropertyChanged += Timer_PropertyChanged;
                    TimerModels.Add(savedTimer);
                }
            }
        }

        private void OnTimerReplaced(TimerModel newTimer)
        {
            if (newTimer != null)
            {
                var oldTimer = TimerModels.FirstOrDefault(timer => timer.Id == newTimer.Id);

                if (oldTimer != null)
                {
                    oldTimer.PropertyChanged -= Timer_PropertyChanged;
                    TimerModels.Remove(oldTimer);
                }

                newTimer.PropertyChanged += Timer_PropertyChanged;
                TimerModels.Add(newTimer);
            }
        }

        private void OnTimerRemoved(Guid timerId)
        {
            if (timerId == Guid.Empty)
            {
                foreach (TimerModel timerModel in TimerModels)
                {
                    timerModel.PropertyChanged -= Timer_PropertyChanged;
                }

                TimerModels.Clear();
            }
            else
            {
                var timer = TimerModels.FirstOrDefault(timer => timer.Id == timerId);

                if (timer != null)
                {
                    timer.PropertyChanged -= Timer_PropertyChanged;
                    TimerModels.Remove(timer);
                }
            }
        }
        #endregion
    }
}
