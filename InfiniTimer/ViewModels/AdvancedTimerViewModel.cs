using InfiniTimer.Common.Components;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.ViewModels
{
    public class AdvancedTimerViewModel
    {
        public AdvancedTimerViewModel(AdvancedTimerModel advancedTimerModel, ViewTimerContent viewTimerContent)
        {
            AdvancedTimerModel = advancedTimerModel;
            viewTimerContent.TimerSection = advancedTimerModel.TimerSection;
        }

        public AdvancedTimerModel AdvancedTimerModel { get; set; }
    }
}
