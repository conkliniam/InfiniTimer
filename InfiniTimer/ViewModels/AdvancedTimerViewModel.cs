using InfiniTimer.Common;
using InfiniTimer.Models.Timers;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class AdvancedTimerViewModel : INotifyPropertyChanged
    {
        public AdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel)
        {
            AdvancedTimerModel = advancedTimerModel;
            NextColor = ColorHelper.ThemeColors[ColorHelper.Primary];
            SetTimer = (ITimerSection timerSection) =>
            {
                AdvancedTimerModel.TimerSection = timerSection;
                OnPropertyChanged(nameof(AdvancedTimerModel.TimerSection));
            };
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set;}
        public Action<ITimerSection> SetTimer { get; private set; }
        public Color NextColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
