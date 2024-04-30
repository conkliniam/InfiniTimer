using InfiniTimer.Models.Timers;
using InfiniTimer.Views;

namespace InfiniTimer.Common.Components;

public partial class ViewTimerContent : ContentView
{
	public ViewTimerContent()
	{
		InitializeComponent();
	}

	private TimerSection _timerSection;

	public TimerSection TimerSection
    {
		get
		{
			return _timerSection;
		}
		set
		{
			_timerSection = value;
			timerSection.Children.Clear();

			if (_timerSection != null )
			{
				BackgroundColor = _timerSection.Depth % 2 == 0
					? ColorHelper.ThemeColors[ColorHelper.Tertiary]
					: ColorHelper.ThemeColors[ColorHelper.Primary];
            }

			if (_timerSection is TimerListSection timerListSection)
			{
				timerSection.Children.Add(new TimerListView(timerListSection));
			}
			else if (_timerSection is SingleTimerSection singleTimerSection)
			{
                timerSection.Children.Add(new SingleTimerView(singleTimerSection));
            }
		}
	}
}