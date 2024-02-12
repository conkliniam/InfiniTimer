namespace InfiniTimer.Common.Components
{
    public class EditLabel : Label
    {
        public EditLabel()
        {
            HorizontalTextAlignment = TextAlignment.Start;
            VerticalTextAlignment = TextAlignment.Center;
            FontAttributes = FontAttributes.Bold;
            FontSize = 16;
            this.FontFamily = AppConstants.ComfortaaRegular;
            TextColor = Colors.White;
        }
    }
}
