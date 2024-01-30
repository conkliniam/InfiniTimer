using InfiniTimer.Enums;
using InfiniTimer.Models;
using InfiniTimer.Models.Timers;
using System.Collections.ObjectModel;

namespace InfiniTimer.ViewModels
{
    public class EditTimerViewModel
    {
        public EditTimerViewModel(TimerModel timerModel = null)
        {
            timerModel ??= new SimpleTimerModel();
            EditTimerModel = new EditTimerModel(timerModel);
            TimerTypes = new ObservableCollection<string>(Enum.GetNames(typeof(TimerType)).ToList());
        }

        public ObservableCollection<String> TimerTypes { get; private set; }

        public EditTimerModel EditTimerModel { get; set; }
    }
}
