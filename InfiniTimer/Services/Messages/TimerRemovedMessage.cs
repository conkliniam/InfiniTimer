using CommunityToolkit.Mvvm.Messaging.Messages;

namespace InfiniTimer.Services.Messages
{
    public class TimerRemovedMessage : ValueChangedMessage<Guid>
    {
        public TimerRemovedMessage(Guid value) : base(value)
        {
        }
    }
}
