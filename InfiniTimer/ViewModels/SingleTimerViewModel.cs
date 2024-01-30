using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class SingleTimerViewModel
    {
        public const int SecondsPerHour = 3600;
        public const int SecondsPerMinute = 60;
        public SingleTimerViewModel(SingleTimerSection timer)
        {
            Timer = timer;
            TimerColorOptions = new ObservableCollection<string>(Enum.GetNames(typeof(TimerColor)).ToList());
            HoursOptions = new ObservableCollection<int>(Enumerable.Range(0, 24));
            MinutesSeconds = new ObservableCollection<int>(Enumerable.Range(0, 60));
        }

        public SingleTimerSection Timer { get; set; }
        public ObservableCollection<string> TimerColorOptions { get; private set; }
        public ObservableCollection<int> HoursOptions { get; private set; }
        public ObservableCollection<int> MinutesSeconds { get; private set; }
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
    }
}
