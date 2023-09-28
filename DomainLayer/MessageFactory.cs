using DataAccessLayer.Utils.Interfaces;
using DomainLayer.Utils.Interfaces;
using ServiceLayer.Enumerations;
using ServiceLayer.Models;

namespace DomainLayer
{
    public class MessageFactory : IMessageFactory
    {
        ISerializationProvider _serializationProvider;
        public MessageFactory(ISerializationProvider serializationProvider)
        {
            _serializationProvider = serializationProvider;
        }

        public string CreateMessageByActionType(Payload payload)
        {

            switch (payload.MessageActionType)
            {
                case MessageActionType.ClientConnectToServer:
                    string serializedPayload = _serializationProvider.SerializeObject(payload);
                    return serializedPayload;



            }
            return string.Empty;
        }
    }
}
