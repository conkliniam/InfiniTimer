

namespace InfiniTimer.Common.Components;

public partial class CircularProgressBar : ContentView
{
	public CircularProgressBar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TotalSecondsProperty = BindableProperty.Create(nameof(TotalSeconds), typeof(int), typeof(CircularProgressBar), propertyChanged: TotalSecondsPropertyChanged);
    public static readonly BindableProperty RemainingSecondsProperty = BindableProperty.Create(nameof(RemainingSeconds), typeof(decimal), typeof(CircularProgressBar), propertyChanged: RemainingSecondsPropertyChanged);
    public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CircularProgressBar));
    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgressBar), propertyChanged: ProgressColorPropertyChanged);
    public static readonly BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(CircularProgressBar));
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBar));

    public int TotalSeconds
    {
        get { return (int)GetValue(TotalSecondsProperty); }
        set { SetValue(TotalSecondsProperty, value); }
    }

    public decimal RemainingSeconds
    {
        get => (decimal)GetValue(RemainingSecondsProperty);
        set => SetValue(RemainingSecondsProperty, value);
    }

    public int Size
    {
        get { return (int)GetValue(SizeProperty); }
        set { SetValue(SizeProperty, value); }
    }

    public int Thickness
    {
        get { return (int)GetValue(ThicknessProperty); }
        set { SetValue(ThicknessProperty, value); }
    }

    public Color ProgressColor
    {
        get { return (Color)GetValue(ProgressColorProperty); }
        set { SetValue(ProgressColorProperty, value); }
    }

    public Color ProgressLeftColor
    {
        get { return (Color)GetValue(ProgressLeftColorProperty); }
        set { SetValue(ProgressLeftColorProperty, value); }
    }

    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == SizeProperty.PropertyName)
        {
            HeightRequest = Size;
            WidthRequest = Size;
        }
    }

    private static void TotalSecondsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as CircularProgressBar;
        control.ProgressBar.TotalSeconds = (int)newValue;
        control.ProgressView.Invalidate();
    }

    private static void RemainingSecondsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as CircularProgressBar;
        control.ProgressBar.RemainingSeconds = (decimal)newValue;
        control.ProgressView.Invalidate();
    }

    private static void ProgressColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as CircularProgressBar;
        control.ProgressBar.ProgressColor = (Color)newValue;
        control.ProgressView.Invalidate();
    }
}