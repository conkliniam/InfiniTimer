using CommunityToolkit.Mvvm.Messaging.Messages;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services.Messages
{
    public class TimerReplacedMessage : ValueChangedMessage<TimerModel>
    {
        public TimerReplacedMessage(TimerModel value) : base(value)
        {
        }
    }
}
