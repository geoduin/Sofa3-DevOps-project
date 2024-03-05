using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;
using Sofa3Devops.Observers;

namespace Sofa3Devops.NotificationStrategy
{
    public interface INotificationStrategy
    {
        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers);
    }
}
