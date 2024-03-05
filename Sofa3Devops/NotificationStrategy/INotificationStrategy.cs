using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.NotificationStrategy
{
    public interface INotificationStrategy
    {
        public void SendNotification(BacklogItem backlogItem);
    }
}
