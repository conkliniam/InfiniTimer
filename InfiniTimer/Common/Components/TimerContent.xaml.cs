using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.ComponentModel;

namespace InfiniTimer.Common.Components;

public partial class TimerContent : ContentView, INotifyPropertyChanged
{
    private ITimerSection _timerSection;
    private int _depth;
    private Action<ITimerSection> _setTimer;

    public TimerContent()
    {
        InitializeComponent();

    }


    public int Depth
    {
        get
        {
            return _depth;
        }
        set
        {
            _depth = value;

            if (_depth >= AppConstants.DepthLimit)
            {
                timerListButton.IsEnabled = false;
                alternateButton.IsEnabled = false;
            }
        }
    }

    public ITimerSection TimerSection
    {
        get
        {
            return _timerSection;
        }
        set
        {
            if (_timerSection != value)
            {
                _timerSection = value;

                timerContent.Children.Clear();

                if (_timerSection == null)
                {
                    buttonContent.IsVisible = true;
                }
                else
                {
                    buttonContent.IsVisible = false;

                    if (_timerSection is AlternatingTimerSection alternatingTimerSection)
                    {
                        timerContent.Children.Add(new AlternatingTimerView(alternatingTimerSection, HandleDelete));
                    }
                    else if (_timerSection is SequentialTimerSection sequentialTimerSection)
                    {
                        timerContent.Children.Add(new SequentialTimerView(sequentialTimerSection, HandleDelete));
                    }
                    else if (_timerSection is SingleTimerSection singleTimerSection)
                    {
                        timerContent.Children.Add(new SingleTimerView(singleTimerSection, HandleDelete));
                    }
                }
            }
        }
    }

    private Action HandleDelete { get; set; }

    public Action<ITimerSection> SetTimer
    {
        get
        {
            return _setTimer;
        }
        set
        {
            _setTimer = value;

            HandleDelete = () =>
            {
                TimerSection = null;
                _setTimer(null);
            };
        }
    }

    public bool ButtonsOnly { get; set; }

    private void AddSingleTimer()
    {
        if (ButtonsOnly)
        {
            SetTimer(new SingleTimerSection(Depth));
        }
        else
        {
            TimerSection = new SingleTimerSection(Depth);
            SetTimer(TimerSection);
        }
    }

    private void AddTimerClicked(object sender, EventArgs e)
    {
        AddSingleTimer();
    }

    private void AddTimerListClicked(object sender, EventArgs e)
    {
        if (ButtonsOnly)
        {
            SetTimer(new SequentialTimerSection(Depth));
        }
        else
        {
            TimerSection = new SequentialTimerSection(Depth);
            SetTimer(TimerSection);
        }
    }

    private void AddTimerAlternatesClicked(object sender, EventArgs e)
    {
        if (ButtonsOnly)
        {
            SetTimer(new AlternatingTimerSection(Depth));
        }
        else
        {
            TimerSection = new AlternatingTimerSection(Depth);
            SetTimer(TimerSection);
        }
    }
}