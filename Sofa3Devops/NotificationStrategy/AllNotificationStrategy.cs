using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;
using Sofa3Devops.Observers;

namespace Sofa3Devops.NotificationStrategy
{
    public class AllNotificationStrategy : INotificationStrategy
    {
        public AllNotificationStrategy() { }
        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers)
        {
            var subscribersPerType = subscribers.Values;
            foreach (var subscriberList in subscribersPerType)
            {
                foreach (var subscriber in subscriberList)
                {
                    subscriber.Notify(title, message);
                }
            }
        }

    }
}
