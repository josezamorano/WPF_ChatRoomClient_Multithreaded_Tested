using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;

namespace PresentationLayer.EventObservers
{
    public class ChatRoomObserver : IChatRoomObserver
    {
        public event Action<ChatRoomCreatedInfo> ChatRoomCreatedEvent;

        public event Action<List<ChatRoom>> ChatRoomsUpdatedEvent;

        public event Action<ChatRoom> ChatRoomDisplayEvent;

        public void ChatRoomNotifyCreation(ChatRoomCreatedInfo chatRoomCreatedInfo)
        {
            ChatRoomCreatedEvent?.Invoke(chatRoomCreatedInfo);
        }

        public void ChatRoomNotifyUpdate(List<ChatRoom> allUpdatedChatRooms)
        {
            ChatRoomsUpdatedEvent?.Invoke(allUpdatedChatRooms);
        }

        public void ChatRoomNotifyDisplay(ChatRoom chatRoom)
        {
            ChatRoomDisplayEvent?.Invoke(chatRoom);
        }
    }
}
