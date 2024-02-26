using CommunityToolkit.Mvvm.Messaging.Messages;

namespace InfiniTimer.Services.Messages
{
    public class StagedTimersChangedMessage : ValueChangedMessage<bool>
    {
        public StagedTimersChangedMessage(bool staged) : base(staged)
        {
        }
    }
}
