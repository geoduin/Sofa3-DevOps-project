using Sofa3Devops.Adapters;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.NotificationStrategy
{
    public class TesterNotificationStrategy : INotificationStrategy
    {
        private INotification notificationHandler;

        public TesterNotificationStrategy(INotification notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public void SendNotification(BacklogItem backlogItem)
        {
            var testers = backlogItem.Sprint.Members.FindAll(m => m is Tester);
            this.notificationHandler.SendNotification($"Update for {backlogItem.Name}", $"{backlogItem.Name} has been updated to {backlogItem.State.ToString()}", DateTime.Now, testers);
        }

    }
}
    