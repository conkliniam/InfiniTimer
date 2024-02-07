using InfiniTimer.Common;
using InfiniTimer.Enums;
using InfiniTimer.Models;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class ColorPickerViewModel
    {
        public ColorPickerViewModel(Action<string, Color, Color> HandleColorSelection)
        {           
            PopulateTimerColors();
            OnChangeColor = HandleColorSelection;
        }

        public ObservableCollection<ColorPickerModel> TimerColorOptions { get; private set; }

        public Action<string, Color, Color> OnChangeColor {  get; set; }

        private void PopulateTimerColors()
        {
            var options = new ObservableCollection<ColorPickerModel>();

            foreach (TimerColor timerColor in Enum.GetValues<TimerColor>())
            {
                var colorPickerModel = new ColorPickerModel()
                {
                    Text = Enum.GetName(timerColor),
                    EnumValue = timerColor,
                };

                if (ColorHelper.TimerColors.TryGetValue(timerColor, out ColorOption value))
                {
                    colorPickerModel.ForegroundColor = value.Light;
                    colorPickerModel.BackgroundColor = value.Dark;
                }
                options.Add(colorPickerModel);
            }
            TimerColorOptions = options;
        }
    }


}
