using PresentationLayer.Utils.Interfaces;
using System;

namespace PresentationLayer.EventObservers
{
    public class ClientLogObserver :IClientLogObserver
    {
        public event Action<string> ClientLogUpdatedEvent;

        public void ServerUserNotifyUpdate(string report)
        {
            ClientLogUpdatedEvent?.Invoke(report);
        }
    }
}
