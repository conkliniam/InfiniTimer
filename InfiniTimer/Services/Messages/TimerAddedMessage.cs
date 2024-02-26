using CommunityToolkit.Mvvm.Messaging.Messages;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services.Messages
{
    public class TimerAddedMessage : ValueChangedMessage<TimerModel>
    {
        public TimerAddedMessage(TimerModel value) : base(value)
        {
        }
    }
}
