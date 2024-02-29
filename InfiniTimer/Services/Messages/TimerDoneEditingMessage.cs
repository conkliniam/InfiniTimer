using CommunityToolkit.Mvvm.Messaging.Messages;
using InfiniTimer.Models.Timers;

namespace InfiniTimer.Services.Messages
{
    public class TimerDoneEditingMessage : ValueChangedMessage<TimerModel>
    {
        public TimerDoneEditingMessage(TimerModel value) : base(value)
        {
        }
    }
}
