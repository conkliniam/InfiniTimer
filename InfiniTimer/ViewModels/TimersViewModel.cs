﻿using InfiniTimer.Enums;
using InfiniTimer.Models.Timers;
using InfiniTimer.Services;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class TimersViewModel
    {
        private readonly ITimerService _timerService;
        public ObservableCollection<TimerModel> TimerModels { get; set; }

        public TimersViewModel(ITimerService timerService)
        {
            _timerService = timerService;
            TimerModels = new ObservableCollection<TimerModel>(_timerService.GetSavedTimers())
            {
                new AdvancedTimerModel
                {
                    Name = "Pomodoro Technique",
                    TimerSection = new AlternatingTimerSection
                    {
                        MainTimerSection = new AlternatingTimerSection
                        {
                            MainTimerSection = new SingleTimerSection
                            {
                                DisplayText = "Work",
                                Color = TimerColor.Green,
                                Seconds = 1500
                            },
                            AlternateTimerSection = new SingleTimerSection
                            {
                                DisplayText = "Break",
                                Color = TimerColor.Red,
                                Seconds = 300
                            },
                            Cycles = 4
                        },
                        AlternateTimerSection = new SingleTimerSection
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
                new AdvancedTimerModel
                {
                    Name = "52/17",
                    TimerSection = new AlternatingTimerSection
                    {
                        MainTimerSection = new SingleTimerSection
                        {
                            DisplayText = "Work",
                            Color = TimerColor.Green,
                            Seconds = 3120

                        },
                        AlternateTimerSection = new SingleTimerSection
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
                new AdvancedTimerModel
                {
                    Name = "Spartacus Timers",
                    TimerSection = new AlternatingTimerSection
                    {
                        MainTimerSection = new AlternatingTimerSection
                        {
                            MainTimerSection = new SingleTimerSection
                            {
                                DisplayText = "Move",
                                Color = TimerColor.Green,
                                Seconds = 60
                            },
                            AlternateTimerSection = new SingleTimerSection
                            {
                                DisplayText = "Get Ready",
                                Color = TimerColor.Red,
                                Seconds = 15
                            },
                            Cycles = 10
                        },
                        AlternateTimerSection = new SingleTimerSection
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
        }
    }
}
