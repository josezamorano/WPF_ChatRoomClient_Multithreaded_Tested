using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace UnitTestChatRoomClient.ServiceLayerTest
{
    public class InputValidatorTest
    {
        IInputValidator _inputValidator;
        public InputValidatorTest()
        {
            _inputValidator = new InputValidator();  
        }

        [Fact]
        public void ValidateClientConnectToServerInputs_CorrectInputs_ReturnsTrue()
        {
            //Arrange
            var clientInput = new ClientInput() 
            { 
                Username = "Test",
                IPAddress = "192.168.1.17",
                Port="60000",
                ChatRoomName = "TestChatRoom",
                GuestSelectedCount = 1,

            };
            //Act
            var actualResult = _inputValidator.ValidateClientConnectToServerInputs(clientInput);

            //Assert
            Assert.True(actualResult.InputsAreValid);
        }


        [Fact]
        public void ValidateClientConnectToServerInputs_CorrectInputValuess_ReturnsTrue()
        {
            //Arramge
            ClientInput clientInputs = new ClientInput()
            {
                ChatRoomName = "test",
                GuestSelectedCount = 50,
                IPAddress = "127.0.0.1",
                Port = "56789",
                Username = "username",


            };
            //Act
            var actualResult = _inputValidator.ValidateClientConnectToServerInputs(clientInputs);
            //Assert
            Assert.True(actualResult.InputsAreValid);
        }





        [Fact]
        public void ValidateClientConnectToServerInputs_InvalidInputs_ReturnsFalse()
        {
            //Arrange
            var clientInput = new ClientInput()
            {
                Username = "Test",
                IPAddress = "192",
                Port = "6",
                ChatRoomName = "TestChatRoom",
                GuestSelectedCount = 1,

            };
            //Act
            var actualResult = _inputValidator.ValidateClientConnectToServerInputs(clientInput);

            //Assert
            Assert.False(actualResult.InputsAreValid);
        }


        [Fact]
        public void ValidateUserCreateChatRoomAndSendInvitesInputs_ValidInputs_ReturnsTrue()
        {
            //Arrange
            var clientInput = new ClientInput()
            {
                Username = "Test",
                IPAddress = "192.168.1.17",
                Port = "60000",
                ChatRoomName = "TestChatRoom",
                GuestSelectedCount = 1,

            };
            //Act
            var actualResult = _inputValidator.ValidateUserCreateChatRoomAndSendInvitesInputs(clientInput);

            //Assert
            Assert.True(actualResult.InputsAreValid);
        }

        [Fact]
        public void ValidateUserCreateChatRoomAndSendInvitesInputs_IncorrectInputs_ReturnsFalse()
        {
            //Arrange
            var clientInput = new ClientInput()
            {
                Username = "Test",
                IPAddress = "192.168.1.17",
                Port = "60000",
                ChatRoomName = "",
                GuestSelectedCount = 0,

            };
            //Act
            var actualResult = _inputValidator.ValidateUserCreateChatRoomAndSendInvitesInputs(clientInput);

            //Assert
            Assert.False(actualResult.InputsAreValid);
        }

       
    }
}
