using ServiceLayer.Models;
using System;
using System.Collections.ObjectModel;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IServerUserObserver
    {
        public event Action<ObservableCollection<ServerUser>> ServerUsersUpdatedEvent;

        public void ServerUserNotifyUpdate(ObservableCollection<ServerUser> serverUsers);
    }
}
