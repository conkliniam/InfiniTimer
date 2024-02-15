using System.ComponentModel;

namespace InfiniTimer.Models.Timers
{
    public interface ITimerSection : INotifyPropertyChanged
    {
        public int Depth { get; set; }
    }
}
