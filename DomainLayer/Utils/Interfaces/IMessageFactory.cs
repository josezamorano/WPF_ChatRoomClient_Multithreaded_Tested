using ServiceLayer.Models;

namespace DomainLayer.Utils.Interfaces
{
    public interface IMessageFactory
    {
        public string CreateMessageByActionType(Payload payload);
    }
}
