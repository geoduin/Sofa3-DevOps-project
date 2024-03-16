using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.NotificationStrategy
{
    public class PONotificationStrategy : INotificationStrategy
    {
        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers)
        {
            var productOwnerNotificationStrategy = subscribers.GetValueOrDefault(typeof(ProductOwner), new List<Subscriber>());
            productOwnerNotificationStrategy.ForEach(subscriber =>subscriber.Notify(title, message));
        }
    }
}
