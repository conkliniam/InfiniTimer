using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.Common
{
    public static class TimerHelper
    {
        public static void AddDefaultTimers(ObservableCollection<TimerModel> Timers)
        {
            Timers.Add(GetPomodoroTimer());
            Timers.Add(GetFiftyTwoSeventeenTimer());
            Timers.Add(GetSparacusTimer());
        }

        public static List<TimerModel> GetDefaultTimers()
        {
            return new List<TimerModel>()
            {
                GetPomodoroTimer(),
                GetFiftyTwoSeventeenTimer(),
                GetSparacusTimer()
            };
        }

        private static AdvancedTimerModel GetPomodoroTimer()
        {
            return new AdvancedTimerModel
            {
                Name = "Pomodoro Technique",
                TimerSection = new AlternatingTimerSection(1)
                {
                    MainTimerSection = new AlternatingTimerSection(2)
                    {
                        MainTimerSection = new SingleTimerSection(3)
                        {
                            DisplayText = "Work",
                            Color = TimerColor.Green,
                            Seconds = 1500
                        },
                        AlternateTimerSection = new SingleTimerSection(3)
                        {
                            DisplayText = "Break",
                            Color = TimerColor.Red,
                            Seconds = 300
                        },
                        Cycles = 4
                    },
                    AlternateTimerSection = new SingleTimerSection(2)
                    {
                        DisplayText = "Break",
                        Color = TimerColor.Red,
                        Seconds = 900
                    },
                    Cycles = 1
                },
                Description = "25 minutes of work, followed by 5 minute breaks, with a 15 minute break after 4 work sessions.",
                AutoRepeat = true,
                AutoContinue = false,
                IsDirty = false,
                IgnoreChanges = false
            };
        }

        private static AdvancedTimerModel GetFiftyTwoSeventeenTimer()
        {
            return new AdvancedTimerModel
            {
                Name = "52/17",
                TimerSection = new AlternatingTimerSection(1)
                {
                    MainTimerSection = new SingleTimerSection(2)
                    {
                        DisplayText = "Work",
                        Color = TimerColor.Green,
                        Seconds = 3120

                    },
                    AlternateTimerSection = new SingleTimerSection(2)
                    {
                        DisplayText = "Break",
                        Color = TimerColor.Red,
                        Seconds = 1020

                    },
                    Cycles = 1
                },
                Description = "52 minutes of work followed by a 17 minute break",
                AutoRepeat = true,
                AutoContinue = false,
                IsDirty = false
            };
        }

        private static AdvancedTimerModel GetSparacusTimer()
        {
            return new AdvancedTimerModel
            {
                Name = "Spartacus Timers",
                TimerSection = new AlternatingTimerSection(1)
                {
                    MainTimerSection = new AlternatingTimerSection(2)
                    {
                        MainTimerSection = new SingleTimerSection(3)
                        {
                            DisplayText = "Move",
                            Color = TimerColor.Green,
                            Seconds = 60
                        },
                        AlternateTimerSection = new SingleTimerSection(3)
                        {
                            DisplayText = "Get Ready",
                            Color = TimerColor.Red,
                            Seconds = 15
                        },
                        Cycles = 10
                    },
                    AlternateTimerSection = new SingleTimerSection(2)
                    {
                        DisplayText = "Rest",
                        Color = TimerColor.Blue,
                        Seconds = 120
                    },
                    Cycles = 3
                },
                Description = "Timers for sparacus workout consisting of 3 sets of 10 60 second exercises",
                AutoRepeat = false,
                AutoContinue = true,
                IsDirty = false
            };
        }
    }
}
