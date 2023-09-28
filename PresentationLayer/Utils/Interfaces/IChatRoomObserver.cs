using ServiceLayer.Models;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IChatRoomObserver
    {

        public event Action<ChatRoomCreatedInfo> ChatRoomCreatedEvent;

        public event Action<List<ChatRoom>> ChatRoomsUpdatedEvent;

        public event Action<ChatRoom> ChatRoomDisplayEvent;

        public void ChatRoomNotifyCreation(ChatRoomCreatedInfo chatRoomCreatedInfo);
        public void ChatRoomNotifyUpdate(List<ChatRoom> allUpdatedChatRooms);

        public void ChatRoomNotifyDisplay(ChatRoom chatRoom);
    }
}
