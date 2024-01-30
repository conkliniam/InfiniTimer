using InfiniTimer.Models.Timers;
using System.ComponentModel;

namespace InfiniTimer.ViewModels
{
    public class IndividualTimerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IndividualTimerViewModel(SimpleTimerModel timer)
        {
            
        }
    }
}
