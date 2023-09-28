using System;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IClientLogObserver
    {
        public event Action<string> ClientLogUpdatedEvent;

        public void ServerUserNotifyUpdate(string report);
    }
}
