using CommunityToolkit.Mvvm.Messaging.Messages;
using Task1QQQ.Models;

namespace Task1QQQ.Messages
{
    public class SubstanceCreatedMessage : ValueChangedMessage<List<Substance>>
    {
        public SubstanceCreatedMessage(List<Substance> value) : base(value)
        {
        }
    }
}
