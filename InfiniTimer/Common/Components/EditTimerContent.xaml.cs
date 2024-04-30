using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.ComponentModel;

namespace InfiniTimer.Common.Components;

public partial class EditTimerContent : ContentView, INotifyPropertyChanged
{
    private TimerSection _timerSection;
    private int _depth;
    private Action<TimerSection> _setTimer;

    public EditTimerContent()
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
            }
        }
    }

    public TimerSection TimerSection
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

                    if (_timerSection is TimerListSection listSection)
                    {
                        timerContent.Children.Add(new EditTimerListView(listSection, HandleDelete));
                    }
                    else if (_timerSection is SingleTimerSection timerSection)
                    {
                        timerContent.Children.Add(new EditSingleTimerView(timerSection, HandleDelete));
                    }
                }
            }
        }
    }

    private Action HandleDelete { get; set; }

    public Action<TimerSection> SetTimer
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

    private void AddListClicked(object sender, EventArgs e)
    {
        if (ButtonsOnly)
        {
            SetTimer(new TimerListSection(Depth));
        }
        else
        {
            TimerSection = new TimerListSection(Depth);
            SetTimer(TimerSection);
        }
    }
}