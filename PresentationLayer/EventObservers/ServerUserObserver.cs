using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.ObjectModel;

namespace PresentationLayer.EventObservers
{
    public class ServerUserObserver : IServerUserObserver
    {

        public event Action<ObservableCollection<ServerUser>> ServerUsersUpdatedEvent;

        public void ServerUserNotifyUpdate(ObservableCollection<ServerUser> serverUsers)
        {
            //Here we declare the event and we insert the parameter List<ServerUser>
            //to be notified to the subscribed classes
            ServerUsersUpdatedEvent?.Invoke(serverUsers);
        }

        
    }
}
