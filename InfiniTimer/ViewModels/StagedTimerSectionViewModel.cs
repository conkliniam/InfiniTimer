using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;
using Plugin.Maui.Audio;

namespace InfiniTimer.ViewModels
{
    public class StagedTimerSectionViewModel : CommonBase
    {
        #region Private Fields
        private AppTheme _currentTheme;
        private bool _prevVisible;
        private bool _nextVisible;
        private decimal _secondsRemaining;
        private bool _timerRunning;
        private Animation _timerAnimation;
        private readonly CircularProgressBar _currentProgress;
        private readonly bool _autoRepeat;
        private readonly bool _autoContinue;
        private readonly Page _page;
        private const string TimerAnimation = "TimerAnimation";
        private IAudioPlayer _loopingAudioPlayer;
        private TimerDisplayModel _previousTimer;
        private TimerDisplayModel _currentTimer;
        private TimerDisplayModel _nextTimer;
        #endregion

        #region Constructors
        public StagedTimerSectionViewModel(TimerSection timerSection,
                                           Application application,
                                           CircularProgressBar currentProgress,
                                           bool autoRepeat,
                                           bool autoContinue,
                                           Page page)
        {
            _currentTheme = application.RequestedTheme;
            NavigableTimers = new NavigableTimers(timerSection);
            InitializeCurrentTimerValues();
            _currentProgress = currentProgress;
            _autoRepeat = autoRepeat;
            _autoContinue = autoContinue;
            _page = page;
        }
        #endregion

        #region Public Properties
        public TimerDisplayModel PreviousTimer
        {
            get
            {
                return _previousTimer;
            }
            set
            {
                if (_previousTimer != value)
                {
                    _previousTimer = value;
                    RaisePropertyChanged(nameof(PreviousTimer));
                }
            }
        }
        public TimerDisplayModel CurrentTimer
        {
            get
            {
                return _currentTimer;
            }
            set
            {
                if (_currentTimer != value)
                {
                    _currentTimer = value;
                    RaisePropertyChanged(nameof(CurrentTimer));
                }
            }
        }

        public TimerDisplayModel NextTimer
        {
            get
            {
                return _nextTimer;
            }
            set
            {
                if (_nextTimer != value)
                {
                    _nextTimer = value;
                    RaisePropertyChanged(nameof(NextTimer));
                }
            }
        }
        public NavigableTimers NavigableTimers { get; set; }

        public decimal SecondsRemaining
        {
            get
            {
                return _secondsRemaining;
            }
            private set
            {
                if (_secondsRemaining != value)
                {
                    _secondsRemaining = value;
                    RaisePropertyChanged(nameof(SecondsRemaining));
                }
            }
        }

        public bool PrevVisible
        {
            get
            {
                return _prevVisible;
            }
            private set
            {
                if (_prevVisible != value)
                {
                    _prevVisible = value;
                    RaisePropertyChanged(nameof(PrevVisible));
                }
            }
        }

        public bool NextVisible
        {
            get
            {
                return _nextVisible;
            }
            private set
            {
                if (_nextVisible != value)
                {
                    _nextVisible = value;
                    RaisePropertyChanged(nameof(NextVisible));
                }
            }
        }

        public bool TimerRunning
        {
            get
            {
                return _timerRunning;
            }
            set
            {
                if (_timerRunning != value)
                {
                    _timerRunning = value;
                    RaisePropertyChanged(nameof(TimerRunning));
                }
            }
        }

        public IAudioPlayer LoopingAudioPlayer
        {
            get
            {
                return _loopingAudioPlayer;
            }
            private set
            {
                if (_loopingAudioPlayer != value)
                {
                    var old = _loopingAudioPlayer;
                    _loopingAudioPlayer = value;

                    if (null != old)
                    {
                        old.PlaybackEnded -= _loopingAudioPlayer_PlaybackEnded;
                    }

                    if (null != _loopingAudioPlayer)
                    {
                        LoopAudio = true;
                        _loopingAudioPlayer.PlaybackEnded += _loopingAudioPlayer_PlaybackEnded;
                    }
                }
            }
        }

        private void _loopingAudioPlayer_PlaybackEnded(object sender, EventArgs e)
        {
            if (LoopAudio)
            {
                var player = sender as IAudioPlayer;
                player?.Play();
            }
        }

        public bool LoopAudio { get; private set; }
        #endregion

        #region Public Methods
        public void GoBack()
        {
            if (TimerRunning)
            {
                _currentProgress.AbortAnimation(TimerAnimation);
                TimerRunning = false;
            }
            NavigableTimers.GoBack();
            InitializeCurrentTimerValues();
        }

        public void GoForward()
        {
            if (TimerRunning)
            {
                _currentProgress.AbortAnimation(TimerAnimation);
                TimerRunning = false;
            }
            NavigableTimers.GoForward();
            InitializeCurrentTimerValues();
        }

        public void StartTimer()
        {
            TimerRunning = true;
            _timerAnimation = new Animation(v => SecondsRemaining = (decimal)v, Convert.ToDouble(SecondsRemaining), 0);
            _timerAnimation.Commit(_currentProgress, TimerAnimation, 16, (uint)(SecondsRemaining * 1000), Easing.Linear, HandleTimerFinished);
        }

        public void PauseTimer()
        {
            TimerRunning = false;
            _currentProgress.AbortAnimation(TimerAnimation);
        }

        public void CancelTimer()
        {
            if (TimerRunning)
            {
                TimerRunning = false;
                _currentProgress.AbortAnimation(TimerAnimation);
            }

            NavigableTimers.Restart();
            InitializeCurrentTimerValues();
        }
        #endregion

        #region Private Methods
        private async void HandleTimerFinished(double arg1, bool arg2)
        {
            try
            {
                if (null == NavigableTimers?.Current || SecondsRemaining > 0)
                {
                    return;
                }

                var timerSound = SoundHelper.SoundInfo[NavigableTimers.Current.Sound];
                var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync((timerSound.FileName + SoundHelper.MP3)));

                if (_autoContinue)
                {
                    // Run Timer Sound for 1 second
                    TimeSpan vibrationLength = TimeSpan.FromSeconds(2.5);
                    Vibration.Default.Vibrate(vibrationLength);

                    string message = $"{NavigableTimers.Current.DisplayText} Finished";


                    if (null != NavigableTimers.Next)
                    {
                        GoForward();
                        StartTimer();
                        message += $", Starting {NavigableTimers.Current.DisplayText}";
                    }

                    await MessageHelper.ShowMessage(message, Colors.Green, Colors.White, 2.5);

                    audioPlayer.Play();
                    await Task.Delay(2500);
                    if (audioPlayer.IsPlaying)
                    {
                        audioPlayer.Stop();
                    }
                }
                else
                {
                    if (null != NavigableTimers.Next)
                    {
                        Vibration.Default.Vibrate();
                        LoopingAudioPlayer = audioPlayer;
                        audioPlayer.Play();
                        bool startNext = await _page.DisplayAlert($"{NavigableTimers.Current.DisplayText} Finished", $"Would you like to start the next timer: {NavigableTimers.Next.DisplayText}?", "Yes", "No");
                        LoopAudio = false;
                        audioPlayer.Stop();
                        GoForward();

                        if (startNext)
                        {
                            StartTimer();
                        }
                    }
                    else
                    {
                        audioPlayer.Play();
                        await MessageHelper.ShowMessage($"{NavigableTimers.Current.DisplayText} Finished", Colors.Green, Colors.White, 2.5);
                        await Task.Delay(2500);
                        audioPlayer.Stop();
                    }

                    // Show alert that timer is done
                    // Give option to start next timer or to just dismiss the alert
                    // Vibrate and run timer sound until alert is closed
                }
            }
            catch (Exception ex)
            {
                await MessageHelper.HandleException(ex);
            }
        }

        private void InitializeCurrentTimerValues()
        {
            SecondsRemaining = NavigableTimers.Current.Seconds;
            NextVisible = NavigableTimers.Next != null;
            PrevVisible = NavigableTimers.Previous != null;
            GetDisplayProperties();
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            _currentTheme = e.RequestedTheme;
            GetDisplayProperties();
        }

        private void GetDisplayProperties()
        {
            if (null != NavigableTimers.Current)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Current.Color];
                CurrentTimer ??= new TimerDisplayModel();

                CurrentTimer.BackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                CurrentTimer.ForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
                CurrentTimer.DisplayText = NavigableTimers.Current.DisplayText;
                CurrentTimer.TotalSeconds = NavigableTimers.Current.Seconds;
            }

            if (null != NavigableTimers.Previous)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Previous.Color];
                PreviousTimer ??= new TimerDisplayModel();
                PreviousTimer.BackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                PreviousTimer.ForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
                PreviousTimer.DisplayText = NavigableTimers.Previous.DisplayText;
                PreviousTimer.TotalSeconds = NavigableTimers.Previous.Seconds;
            }

            if (null != NavigableTimers.Next)
            {
                var colors = ColorHelper.TimerColors[NavigableTimers.Next.Color];
                NextTimer ??= new TimerDisplayModel();
                NextTimer.BackColor = _currentTheme == AppTheme.Light ? colors.Light : colors.Dark;
                NextTimer.ForeColor = _currentTheme == AppTheme.Light ? colors.Dark : colors.Light;
                NextTimer.DisplayText = NavigableTimers.Next.DisplayText;
                NextTimer.TotalSeconds = NavigableTimers.Next.Seconds;
            }
        }
        #endregion
    }
}
