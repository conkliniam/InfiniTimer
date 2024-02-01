using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InfiniTimer.ViewModels
{
    public class SingleTimerViewModel : INotifyPropertyChanged
    {
        public const int SecondsPerHour = 3600;
        public const int SecondsPerMinute = 60;
        private readonly ResourceDictionary _resources;
        private string _color;
        private Color _background;
        private Color _foreground;

        public SingleTimerViewModel(SingleTimerSection timer, ResourceDictionary resources)
        {
            Timer = timer;
            _resources = resources;
            TimerColorOptions = new ObservableCollection<string>(Enum.GetNames(typeof(TimerColor)).ToList());
            HoursOptions = new ObservableCollection<int>(Enumerable.Range(0, 24));
            MinutesSeconds = new ObservableCollection<int>(Enumerable.Range(0, 60));
            _color = Enum.GetName(typeof(TimerColor), Timer.Color);
            SetColors();
        }

        public SingleTimerSection Timer { get; set; }
        public ObservableCollection<string> TimerColorOptions { get; private set; }
        public ObservableCollection<int> HoursOptions { get; private set; }
        public ObservableCollection<int> MinutesSeconds { get; private set; }
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
                    OnPropertyChanged(nameof(ForegroundColor));
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
                    OnPropertyChanged(nameof(BackgroundColor));
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
                    OnPropertyChanged(nameof(Color));
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
                return (Timer.Seconds % SecondsPerHour) / SecondsPerMinute;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this,
                 new PropertyChangedEventArgs(propName));
        }

        private void SetColors()
        {
            switch (Timer.Color)
            {
                case TimerColor.White:
                    if (_resources.TryGetValue("White", out var white))
                    {
                        ForegroundColor = (Color)white;
                    }
                    if (_resources.TryGetValue("Black", out var black))
                    {
                        BackgroundColor = (Color)black;
                    }
                    break;
                default:
                    if (_resources.TryGetValue("Light" + _color, out var foreColor))
                    {
                        ForegroundColor = (Color)foreColor;
                    }
                    if (_resources.TryGetValue("Dark" + _color, out var backColor))
                    {
                        BackgroundColor = (Color)backColor;
                    }
                    break;
            }
        }

        public void HandleColorSelection(string colorText, Color backgroundColor, Color foregroundColor)
        {
            Color = colorText;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }
    }
}
