using ServiceLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IInputValidator
    {
        ClientInputValidationReport ValidateClientConnectToServerInputs(ClientInput clientInput);

        ClientInputValidationReport ValidateUserCreateChatRoomAndSendInvitesInputs(ClientInput clientInput);
    }
}
