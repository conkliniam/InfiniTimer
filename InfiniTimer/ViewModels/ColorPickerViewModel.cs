using InfiniTimer.Enums;
using InfiniTimer.Models;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class ColorPickerViewModel
    {
        private ResourceDictionary _resources;
        public ColorPickerViewModel(ResourceDictionary resources, Action<string, Color, Color> HandleColorSelection)
        {
            _resources = resources;            
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

                switch (timerColor)
                {
                    case TimerColor.White:
                        if (_resources.TryGetValue("White", out var white))
                        {
                            colorPickerModel.ForegroundColor = (Color)white;
                        }
                        if (_resources.TryGetValue("Black", out var black))
                        {
                            colorPickerModel.BackgroundColor = (Color)black;
                        }
                        break;
                    default:
                        if (_resources.TryGetValue("Light" + colorPickerModel.Text, out var foreColor))
                        {
                            colorPickerModel.ForegroundColor = (Color)foreColor;
                        }
                        if (_resources.TryGetValue("Dark" + colorPickerModel.Text, out var backColor))
                        {
                            colorPickerModel.BackgroundColor = (Color)backColor;
                        }
                        break;
                }
                options.Add(colorPickerModel);
            }
            TimerColorOptions = options;
        }
    }


}
