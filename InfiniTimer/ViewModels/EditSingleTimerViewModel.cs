using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditSingleTimerViewModel : CommonBase
    {
        public const int SecondsPerHour = 3600;
        public const int SecondsPerMinute = 60;
        private string _color;
        private string _sound;
        private Color _background;
        private Color _foreground;

        public EditSingleTimerViewModel(SingleTimerSection timer)
        {
            Timer = timer;
            TimerColorOptions = new ObservableCollection<string>(Enum.GetNames(typeof(TimerColor)).ToList());
            HoursOptions = new ObservableCollection<int>(Enumerable.Range(0, 24));
            MinutesSeconds = new ObservableCollection<int>(Enumerable.Range(0, 60));
            _color = Enum.GetName(typeof(TimerColor), Timer.Color);
            _sound = SoundHelper.SoundInfo[Timer.Sound].DisplayName;
            SetColors();
        }

        public SingleTimerSection Timer { get; set; }
        public ObservableCollection<string> TimerColorOptions { get; private set; }
        public ObservableCollection<int> HoursOptions { get; private set; }
        public ObservableCollection<int> MinutesSeconds { get; private set; }
        public Thickness Margin { get; private set; }
        public Color ForegroundColor
        {
            get
            {
                return _foreground;
            }
            set
            {
                if (_foreground != value)
                {
                    _foreground = value;
                    RaisePropertyChanged(nameof(ForegroundColor));
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return _background;
            }
            set
            {
                if (_background != value)
                {
                    _background = value;
                    RaisePropertyChanged(nameof(BackgroundColor));
                }
                
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (value != _color)
                {
                    _color = value;

                    if (Enum.TryParse<TimerColor>(_color, out var newColor))
                    {
                        Timer.Color = newColor;
                    }
                    RaisePropertyChanged(nameof(Color));
                }
            }
        }

        public string Sound
        {
            get
            {
                return _sound;
            }
            set
            {
                if (_sound != value)
                {
                    _sound = value;
                    RaisePropertyChanged(nameof(Sound));
                }
            }
        }

        public int Hours
        {
            get
            {
                return Timer.Seconds / SecondsPerHour;
            }
            set
            {
                if (value != Hours)
                {
                    Timer.Seconds = Timer.Seconds + (SecondsPerHour * (value - Hours));
                }
            }
        }

        public int Minutes
        {
            get
            {
                return Timer.Seconds % SecondsPerHour / SecondsPerMinute;
            }
            set
            {
                if (value != Minutes)
                {
                    Timer.Seconds = Timer.Seconds + (SecondsPerMinute * (value - Minutes));
                }
            }
        }
        public int Seconds
        {
            get
            {
                return Timer.Seconds % SecondsPerMinute;
            }
            set
            {
                if (value != Seconds)
                {
                    Timer.Seconds = Timer.Seconds + value - Seconds;
                }
            }
        }

        private void SetColors()
        {
            if (ColorHelper.TimerColors.ContainsKey(Timer.Color))
            {
                var colorOption = ColorHelper.TimerColors[Timer.Color];
                Color = colorOption.Name;
                BackgroundColor = colorOption.Dark;
                ForegroundColor = colorOption.Light;
            }
        }

        public void HandleColorSelection(string colorText, Color backgroundColor, Color foregroundColor)
        {
            Color = colorText;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }

        public void HandleSoundSelection(TimerSound timerSound, string soundText)
        {
            Timer.Sound = timerSound;
            Sound = soundText;
        }
    }
}
