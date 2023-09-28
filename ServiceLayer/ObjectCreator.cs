using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class ObjectCreator : IObjectCreator
    {
        public Payload CreatePayload(MessageActionType messageActionType, string username)
        {
            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username
            };
            return payload;
        }

        public Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId)
        {
            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username,
                UserId = mainUserId,
            };
            return payload;
        }

        public Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId, ChatRoom chatRoom)
        {
            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ClientUsername = username,
                UserId = mainUserId,
                ChatRoomCreated = chatRoom
            };
            return payload;
        }

        public Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, string messageToChatRoom)
        {

            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ChatRoomCreated = chatRoom,
                MessageToChatRoom = messageToChatRoom
            };
            return payload;
        }
        public Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, Invite invite)
        {

            Payload payload = new Payload()
            {
                MessageActionType = messageActionType,
                ChatRoomCreated = chatRoom,
                InviteToGuestUser = invite
            };
            return payload;
        }


        public ChatRoom CreateChatRoom(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<Invite> allInvitesSentToGuestUsers)
        {
            List<ServerUser> allActiveUsersInChatRoom = new List<ServerUser>() { chatRoomCreatorMainServerUser };
            ChatRoom chatRoom = new ChatRoom()
            {
                ChatRoomName = chatRoomName,
                ChatRoomStatus = ChatRoomStatus.Created,
                Creator = chatRoomCreatorMainServerUser,
                ConversationRecord = string.Empty,
                AllActiveUsersInChatRoom = allActiveUsersInChatRoom,
                AllInvitesSentToGuestUsers = allInvitesSentToGuestUsers
            };

            return chatRoom;
        }

        public ChatRoom CreateChatRoom(string username, Guid userId, string chatRoomName, Guid chatRoomId)
        {
            string ChatRoomIdentifier = chatRoomName + "_" + chatRoomId;
            ServerUser mainUser = new ServerUser()
            {
                ServerUserID = userId,
                Username = username,
            };
            ChatRoom chatRoom = new ChatRoom()
            {
                Creator = mainUser,
                ChatRoomId = chatRoomId,
                ChatRoomName = chatRoomName,
                ChatRoomIdentifierNameId = ChatRoomIdentifier,

            };
            return chatRoom;
        }

        public List<Invite> CreateAllInvitesForAllGuestServerUsers(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<ServerUser> allSelectedGuestUsers)
        {
            List<Invite> allInvitesForAllGuests = new List<Invite>();
            foreach (ServerUser serverUser in allSelectedGuestUsers)
            {
                var invite = new Invite()
                {
                    ChatRoomCreator = chatRoomCreatorMainServerUser,
                    GuestServerUser = serverUser,
                    ChatRoomName = chatRoomName,
                    InviteStatus = InviteStatus.CreatedNotSent
                };
                allInvitesForAllGuests.Add(invite);
            }
            return allInvitesForAllGuests;
        }


    }
}
