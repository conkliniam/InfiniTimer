using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.ViewModels;

namespace InfiniTimer;

public partial class TimersPage : ContentPage
{
    public TimersPage()
    {
        InitializeComponent();
        BindingContext = new TimersViewModel();

        List<TimerModel> timersList = new()
        {
            new AlternatingTimerModel
            {
                Name = "Pomodoro Technique",
                TimerGroup = new TimerGroup
                {
                    MainTimerSection = new TimerGroup
                    {
                        MainTimerSection = new IndividualTimer
                        {
                            DisplayText = "Work",
                            Color = TimerColor.Green,
                            Seconds = 1500
                        },
                        AlternateTimerSection = new IndividualTimer
                        {
                            DisplayText = "Break",
                            Color = TimerColor.Red,
                            Seconds = 300
                        },
                        Cycles = 4
                    },
                    AlternateTimerSection = new IndividualTimer
                    {
                        DisplayText = "Break",
                        Color = TimerColor.Red,
                        Seconds = 900
                    },
                    Cycles = 1
                },
                Description = "25 minutes of work, followed by 5 minute breaks, with a 15 minute break after 4 work sessions.",
                AutoRepeat = true,
                AutoContinue = false
            },
            new AlternatingTimerModel
            {
                Name = "52/17",
                TimerGroup = new TimerGroup
                {
                    MainTimerSection = new IndividualTimer
                    {
                        DisplayText = "Work",
                        Color = TimerColor.Green,
                        Seconds = 3120

                    },
                    AlternateTimerSection = new IndividualTimer
                    {
                        DisplayText = "Break",
                        Color = TimerColor.Red,
                        Seconds = 1020

                    },
                    Cycles = 1
                },
                Description = "52 minutes of work followed by a 17 minute break",
                AutoRepeat = true,
                AutoContinue = false
            },
            new AlternatingTimerModel
            {
                Name = "Spartacus Timers",
                TimerGroup = new TimerGroup
                {
                    MainTimerSection = new TimerGroup
                    {
                        MainTimerSection = new IndividualTimer
                        {
                            DisplayText = "Move",
                            Color = TimerColor.Green,
                            Seconds = 60
                        },
                        AlternateTimerSection = new IndividualTimer
                        {
                            DisplayText = "Get Ready",
                            Color = TimerColor.Red,
                            Seconds = 15
                        },
                        Cycles = 10
                    },
                    AlternateTimerSection = new IndividualTimer
                    {
                        DisplayText = "Rest",
                        Color = TimerColor.Blue,
                        Seconds = 120
                    },
                    Cycles = 3
                },
                Description = "Timers for sparacus workout consisting of 3 sets of 10 60 second exercises",
                AutoRepeat = false,
                AutoContinue = true
            }
        };

        listTimers.ItemsSource = timersList;//((TimersViewModel)BindingContext).TimerModels;
    }
}