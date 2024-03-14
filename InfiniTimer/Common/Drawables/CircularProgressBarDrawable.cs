using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniTimer.Common.Drawables
{
    public class CircularProgressBarDrawable : BindableObject, IDrawable
    {
        public static readonly BindableProperty TotalSecondsProperty = BindableProperty.Create(nameof(TotalSeconds), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty RemainingSecondsProperty = BindableProperty.Create(nameof(RemainingSeconds), typeof(decimal), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(int), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty ProgressLeftColorProperty = BindableProperty.Create(nameof(ProgressLeftColor), typeof(Color), typeof(CircularProgressBarDrawable));
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBarDrawable));

        public int TotalSeconds
        {
            get => (int)GetValue(TotalSecondsProperty);
            set => SetValue(TotalSecondsProperty, value);
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

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float effectiveSize = Size - Thickness;
            float x = Thickness / 2;
            float y = Thickness / 2;
            var percent = RemainingSeconds / TotalSeconds * 100;

            if (percent < 0)
            {
                percent = 0;
            }
            else if (percent > 360)
            {
                percent = 360;
            }

            if (percent < 100)
            {
                var angle = GetAngle(percent);

                canvas.StrokeColor = ProgressLeftColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);

                // Draw arc
                canvas.StrokeColor = ProgressColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawArc(x, y, effectiveSize, effectiveSize, 90, angle, true, false);
            }
            else
            {
                // Draw circle
                canvas.StrokeColor = ProgressColor;
                canvas.StrokeSize = Thickness;
                canvas.DrawEllipse(x, y, effectiveSize, effectiveSize);
            }

            // Make the percentage always the same size in relation to the size of the progress bar
            string timeRemaining = TimerHelper.GetTimerDisplay(RemainingSeconds);
            float fontSize = effectiveSize / (timeRemaining.Length > 5 ? 4.5f : 2.86f);
            canvas.FontSize = fontSize;
            canvas.FontColor = TextColor;

            // Vertical text align the text, and we need a correction factor of around 1.15 to have it aligned properly
            // Note: The VerticalAlignment.Center property of the DrawString method seems to have no effect
            float horizontalPosition = effectiveSize / 10.0f;
            canvas.DrawString(timeRemaining, horizontalPosition, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Left, VerticalAlignment.Center);
        }

        private float GetAngle(decimal progress)
        {
            float factor = 90f / 25f;

            if (progress > 75)
            {
                return -180 - (((float)progress - 75) * factor);
            }
            else if (progress > 50)
            {
                return -90 - (((float)progress - 50) * factor);
            }
            else if (progress > 25)
            {
                return 0 - (((float)progress - 25) * factor);
            }
            else
            {
                return 90 - ((float)progress * factor);
            }
        }
    }
}
