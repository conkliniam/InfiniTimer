using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class AdvancedTimerViewModel : INotifyPropertyChanged
    {
        public AdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel, TimerContent timerContent)
        {
            AdvancedTimerModel = advancedTimerModel;
            NextColor = ColorHelper.ThemeColors[ColorHelper.Primary];
            timerContent.TimerSection = advancedTimerModel.TimerSection;
            timerContent.SetTimer = (ITimerSection timerSection) =>
            {
                AdvancedTimerModel.TimerSection = timerSection;
                OnPropertyChanged(nameof(AdvancedTimerModel.TimerSection));
            };
            timerContent.Depth = 1;
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set;}
        public Color NextColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
