using InfiniTimer.Enums;

namespace InfiniTimer.Models
{
    public class ColorPickerModel
    {
        public string Text {  get; set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public TimerColor EnumValue { get; set; }
    }
}
