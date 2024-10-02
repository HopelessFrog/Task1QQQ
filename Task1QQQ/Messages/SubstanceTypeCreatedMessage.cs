using CommunityToolkit.Mvvm.Messaging.Messages;
using Task1QQQ.Models;

namespace Task1QQQ.Messages
{
    public class SubstanceTypeCreatedMessage : ValueChangedMessage<List<SubstanceType>>
    {
        public SubstanceTypeCreatedMessage(List<SubstanceType> value) : base(value)
        {
        }
    }
}
