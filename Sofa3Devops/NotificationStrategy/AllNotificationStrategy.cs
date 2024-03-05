using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;

namespace Sofa3Devops.NotificationStrategy
{
    public class AllNotificationStrategy : INotificationStrategy
    {
        private INotification notificationHandler;

        public AllNotificationStrategy(INotification notificationHandler)
        {
            this.notificationHandler = notificationHandler; 
        }
        public void SendNotification(BacklogItem backlogItem)
        {
            this.notificationHandler.SendNotification($"Update for {backlogItem.Name}", $"{backlogItem.Name} has been updated to {backlogItem.State.ToString()}", DateTime.Now, backlogItem.Sprint.Members);
        }

    }
}
