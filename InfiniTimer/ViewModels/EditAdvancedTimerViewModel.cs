using InfiniTimer.Common;
using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class EditAdvancedTimerViewModel : CommonBase
    {
        public EditAdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel, EditTimerContent timerContent)
        {
            AdvancedTimerModel = advancedTimerModel;
            NextColor = ColorHelper.ThemeColors[ColorHelper.Primary];
            timerContent.TimerSection = advancedTimerModel.TimerSection;
            timerContent.SetTimer = (TimerSection timerSection) =>
            {
                AdvancedTimerModel.TimerSection = timerSection;
                RaisePropertyChanged(nameof(AdvancedTimerModel.TimerSection));
            };
            timerContent.Depth = 1;
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set;}
        public Color NextColor { get; set; }
    }
}
