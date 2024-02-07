using InfiniTimer.Models.Timers;
using InfiniTimer.Views;
using System.ComponentModel;

namespace InfiniTimer.Common.Components;

public partial class TimerContent : ContentView, INotifyPropertyChanged
{
    public TimerContent()
    {
        InitializeComponent();

    }

    public static readonly BindableProperty DepthProperty =
        BindableProperty.Create(nameof(Depth), typeof(int), typeof(int));

    public int Depth
    {
        get
        {
            return (int)GetValue(DepthProperty);
        }
        set
        {
            SetValue(DepthProperty, value);
        }
    }

    public static readonly BindableProperty TimerSectionProperty =
        BindableProperty.Create(nameof(TimerSection), typeof(ITimerSection), typeof(SingleTimerSection));

    public ITimerSection TimerSection
    {
        get
        {
            return (ITimerSection)GetValue(TimerSectionProperty);
        }
        set
        {
            if (TimerSection != value)
            {
                SetValue(TimerSectionProperty, value);

                timerContent.Children.Clear();

                if (TimerSection == null)
                {
                    buttonContent.IsVisible = true;
                }
                else
                {
                    buttonContent.IsVisible = false;

                    if (TimerSection is AlternatingTimerSection alternatingTimerSection)
                    {
                        timerContent.Children.Add(new AlternatingTimerView(alternatingTimerSection));
                    }
                    else if (TimerSection is SequentialTimerSection sequentialTimerSection)
                    {
                        timerContent.Children.Add(new SequentialTimerView(sequentialTimerSection));
                    }
                    else if (TimerSection is SingleTimerSection singleTimerSection)
                    {
                        timerContent.Children.Add(new SingleTimerView(singleTimerSection));
                    }
                }
            }
        }
    }

    public Action<ITimerSection> SetTimer { get; set; }

    public bool ButtonsOnly { get; set; }

    private void AddTimerClicked(object sender, EventArgs e)
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