using DataAccessLayer.IONetwork;
using DataAccessLayer.Utils.Interfaces;
using Newtonsoft.Json;
using ServiceLayer.Models;

namespace UnitTestChatRoomClient.DataAccessLayerTest
{
    public class SerializationProviderTest
    {
        ISerializationProvider _serializationProvider;
        public SerializationProviderTest()
        {
            _serializationProvider = new SerializationProvider();
        }

        [Fact]
        public void SerializingObject_ReturnsOK()
        {
            //Arrange
            var student = new { Id = 1, FirstName = "James", LastName = "Bond" };
            string error = "ERROR";
            //Act
            var actualResult =_serializationProvider.SerializeObject(student);
            //Assert

            Assert.DoesNotContain(error, actualResult);

        }

        [Fact]
        public void DeserializingANonObject_ThrowsException()
        {
            //Arrange
            string student = "void Info(){ }";

            //Act 
            var act = () => { _serializationProvider.DeserializeObject<string>(student); };
            //Assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(act);
        }

        public class Student { public int Id = 1; public string FirstName = "James";public string LastName = "Bond"; };

        [Fact]
        public void DeserializingAnObject_ReturnsOK()
        {
            //Arrange
            var std = new Student();
            var stdStr = JsonConvert.SerializeObject(std);
           
            //Act 
            Student actualResult= _serializationProvider.DeserializeObject<Student>(stdStr);
            //Assert
            var name = actualResult.FirstName;

            Assert.True(name.Equals(std.FirstName));
        }

        [Fact]
        public void SerializeObject_CorrectInputObject_ReturnsOk()
        {
            //Arrange
            string expectedSubstring = "username";
            Payload payload = new Payload()
            {
                ClientUsername = expectedSubstring,
            };
            //Act
            var actualResult = _serializationProvider.SerializeObject(payload);
            //Assert
            Assert.Contains(expectedSubstring, actualResult);

        }

        [Fact]
        public void SerializeObject_StringInputObject_ReturnsOK()
        {
            //Arrange
            string expectedSubstring = "username";

            //Act
            var actualResult = _serializationProvider.SerializeObject(expectedSubstring);
            //Assert
            Assert.Contains(expectedSubstring, actualResult);

        }

        [Fact]
        public void SerializeObject_NullInputObject_ReturnsOk()
        {
            //Arrange
            Payload payload = null;

            //Act
            var actualResult = _serializationProvider.SerializeObject(payload);
            //Assert
            Assert.Contains("null", actualResult);

        }


        [Fact]
        public void DeserializeObject_CorrectInputString_ReturnsOk()
        {
            //arrange
            string serializedObject = "{\"MessageActionType\":0,\"ClientUsername\":\"username\",\"UserId\":null,\"ActiveServerUsers\":null,\"ChatRoomCreated\":null,\"InviteToGuestUser\":null,\"MessageToChatRoom\":null,\"ServerUserDisconnected\":null,\"ServerUserRemovedFromChatRoom\":null}";
            //Act
            var actualResult = _serializationProvider.DeserializeObject<Payload>(serializedObject);
            //Assert
            Assert.IsType<Payload>(actualResult);
        }

       
    }
}
