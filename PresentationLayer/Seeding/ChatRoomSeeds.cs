using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PresentationLayer.Seeding
{
    public static class ChatRoomSeeds
    {
        public static async Task<ObservableCollection<ChatRoom>> GetAllChatRoomsSeedsAsyncTask()
        {
            var result = await Task.Factory.StartNew(() => 
            {
                ObservableCollection<ChatRoom> allChatRooms = new ObservableCollection<ChatRoom>();


                List<ChatRoom> allChatRoomSeeds = GetAllChatRoomSeeds();
                foreach (var chatRoom in allChatRoomSeeds)
                {
                    chatRoom.AllActiveUsersInChatRoomCount = chatRoom.AllActiveUsersInChatRoom.Count;
                    allChatRooms.Add(chatRoom);
                }

                return allChatRooms;
            });

            return result;
        }

        public static int GetAllChatRoomSeedsCount()
        {
            return GetAllChatRoomSeeds().Count;
        }

        private static List<ChatRoom> GetAllChatRoomSeeds()
        {
            List<ChatRoom> allChatRooms = new List<ChatRoom>();
            ChatRoom chatRoom1 = new ChatRoom()
            {
                ChatRoomName = "ABC1",
                ChatRoomId = Guid.NewGuid(),
                ChatRoomIdentifierNameId = "abc1-111",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc" }, new ServerUser() { Username = "mno" }, new ServerUser() { Username = "Jonathan" }, new ServerUser() { Username = "Surex" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "TOM" }, InviteStatus = InviteStatus.Accepted }, }

            };
            ChatRoom chatRoom2 = new ChatRoom()
            {
                ChatRoomName = "ABC2",
                ChatRoomId = Guid.NewGuid(),
                ChatRoomIdentifierNameId = "abc2-112",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc1" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Mark" }, InviteStatus = InviteStatus.SentAndPendingResponse } }

            };
            ChatRoom chatRoom3 = new ChatRoom()
            {
                ChatRoomName = "ABC3",
                ChatRoomId = Guid.NewGuid(),
                ChatRoomIdentifierNameId = "abc3-113",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc2" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Pam" }, InviteStatus = InviteStatus.SentAndPendingResponse }, }

            };
            ChatRoom chatRoom4 = new ChatRoom()
            {
                ChatRoomName = "ABC4",
                ChatRoomId = Guid.NewGuid(),
                ChatRoomIdentifierNameId = "abc4-114",
                ChatRoomStatus = ChatRoomStatus.OpenActive,
                AllActiveUsersInChatRoom = new List<ServerUser> { new ServerUser() { Username = "abc3" }, new ServerUser() { Username = "mno" } },
                AllInvitesSentToGuestUsers = new List<Invite> { new Invite() { GuestServerUser = new ServerUser() { Username = "Jack" }, InviteStatus = InviteStatus.SentAndPendingResponse }, }

            };
            allChatRooms.Add(chatRoom1);
            allChatRooms.Add(chatRoom2);
            allChatRooms.Add(chatRoom3);
            allChatRooms.Add(chatRoom4);
            return allChatRooms;
        }

    }
}
