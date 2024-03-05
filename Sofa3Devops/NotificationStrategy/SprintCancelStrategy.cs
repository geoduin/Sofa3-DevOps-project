using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;

namespace Sofa3Devops.NotificationStrategy
{
    public class SprintCancelStrategy : ISprintNotificationStrategy

    {

        private INotificationAdapter notificationHandler;
        public SprintCancelStrategy(INotificationAdapter notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public void SendNotification(Sprint sprint)
        {
            this.notificationHandler.SendNotification($"Update for {sprint.Name}", $"The release for {sprint.Name} has been cancelled", DateTime.Now, sprint.Members);
        }
    }
}
