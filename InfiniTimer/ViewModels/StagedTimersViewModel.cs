using CommunityToolkit.Mvvm.Messaging;
using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using InfiniTimer.Services.Messages;
using InfiniTimer.Views;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace InfiniTimer.ViewModels
{
    public class StagedTimersViewModel : CommonBase
    {
        #region Private Fields
        private readonly IStagedTimerService _stagedTimerService;
        private readonly ISavedTimerService _savedTimerService;
        private readonly Image _logoImage;
        private readonly VerticalStackLayout _stagedTimerLayout;
        private readonly Page _page;
        private Animation _rotateAnimation;
        private GridLength _topHeight;
        private GridLength _midHeight;
        private bool _welcomeVisible;
        private ObservableCollection<TimerModel> _stagedTimers;
        private Dictionary<Guid, StagedTimerView> _stagedTimerViews;
        #endregion

        #region Constructor
        public StagedTimersViewModel(IStagedTimerService stagedTimerService,
                                     ISavedTimerService savedTimerService,
                                     Image logoImage,
                                     VerticalStackLayout stagedTimerLayout,
                                     Page page)
        {
            _stagedTimerService = stagedTimerService;
            _savedTimerService = savedTimerService;
            _logoImage = logoImage;
            _stagedTimerLayout = stagedTimerLayout;
            _page = page;
            StagedTimers = _stagedTimerService.GetStagedTimers();
            InitializeContent();
            RegisterForMessages();
            FillStagedTimerViews();
        }
        #endregion

        #region Public Properties
        public ObservableCollection<TimerModel> StagedTimers
        {
            get
            {
                return _stagedTimers;
            }
            set
            {
                if (_stagedTimers != value)
                {
                    var old = _stagedTimers;
                    _stagedTimers = value;

                    if (old != null)
                    {
                        old.CollectionChanged -= StagedTimers_CollectionChanged;
                    }

                    if (_stagedTimers != null)
                    {
                        _stagedTimers.CollectionChanged += StagedTimers_CollectionChanged;
                    }
                }
            }
        }

        public GridLength TopHeight
        {
            get
            {
                return _topHeight;
            }
            set
            {
                _topHeight = value;
                RaisePropertyChanged(nameof(TopHeight));
            }
        }

        public GridLength MidHeight
        {
            get
            {
                return _midHeight;
            }
            set
            {
                _midHeight = value;
                RaisePropertyChanged(nameof(MidHeight));
            }
        }

        public bool WelcomeVisible
        {
            get
            {
                return _welcomeVisible;
            }
            set
            {
                if (_welcomeVisible != value)
                {
                    _welcomeVisible = value;
                    RaisePropertyChanged(nameof(WelcomeVisible));
                }
            }
        }
        #endregion

        #region Public Methods
        public void UnstageAll()
        {
            _stagedTimerLayout.Clear();
            _stagedTimerService.UnstageAllTimers();
        }
        #endregion

        #region Private Methods
        private void StagedTimers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (null != e.OldItems && e.OldItems.Count > 0)
            {
                RemoveTimers(e.OldItems);
            }

            if (null != e.NewItems && e.NewItems.Count > 0)
            {
                AddTimers(e.NewItems);
            }
        }

        private void AddTimers(IList newTimers)
        {
            foreach (TimerModel timerModel in newTimers)
            {
                var stagedTimerView = new StagedTimerView(timerModel, _savedTimerService, _stagedTimerService, _page);

                _stagedTimerLayout.Children.Add(stagedTimerView);
                _stagedTimerViews[timerModel.Id] = stagedTimerView;
            }
        }

        private void RemoveTimers(IList oldTimers)
        {
            foreach (TimerModel timerModel in oldTimers)
            {
                _stagedTimerViews.TryGetValue(timerModel.Id, out var view);

                if (view != null)
                {
                    _stagedTimerLayout.Children.Remove(view);
                    _stagedTimerViews.Remove(timerModel.Id);
                }
            }
        }

        private void FillStagedTimerViews()
        {
            _stagedTimerViews = [];
            _stagedTimerLayout.Clear();

            if (StagedTimers.Any())
            {
                AddTimers(StagedTimers);
            }
        }

        private void RegisterForMessages()
        {
            WeakReferenceMessenger.Default.Register<StagedTimersChangedMessage>(this, (r, m) =>
            {
                OnStagedTimersChange(m.Value);
            });

            WeakReferenceMessenger.Default.Register<TimerDoneEditingMessage>(this, (r, m) =>
            {
                OnTimerDoneEditing(m.Value);
            });
        }

        private void OnTimerDoneEditing(TimerModel timer)
        {
            if (timer.IsStaged)
            {
                timer.RaisePropertyChanged(nameof(TimerModel.Name));
            }
        }

        private void OnStagedTimersChange(bool staged)
        {
            if (staged && WelcomeVisible)
            {
                DisplayTimers();
            }
            else if (!staged && !WelcomeVisible)
            {
                if (!StagedTimers.Any())
                {
                    DisplayWelcome();
                }
            }
        }

        private void InitializeContent()
        {
            _rotateAnimation = new Animation(v => _logoImage.Rotation = v, 0, 360);

            if (StagedTimers.Any())
            {
                DisplayTimers();
            }
            else
            {
                DisplayWelcome();
            }
        }

        private void DisplayWelcome()
        {
            TopHeight = new GridLength(0);
            MidHeight = new GridLength(11, GridUnitType.Star);
            WelcomeVisible = true;
            _rotateAnimation.Commit(_logoImage, "RotateAnimation", 16, 4000, null, null, () => !StagedTimers.Any());
        }

        private void DisplayTimers()
        {
            TopHeight = new GridLength(1, GridUnitType.Star);
            MidHeight = new GridLength(10, GridUnitType.Star);
            WelcomeVisible = false;
        }
        #endregion
    }
}
