using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.NotificationStrategy
{
    public class LeadDeveloperNotificationStrategy : INotificationStrategy
    {
        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers)
        {
            var scrumMasterNotification = subscribers.GetValueOrDefault(typeof(Developer), new List<Subscriber>());
            scrumMasterNotification.ForEach((subscriber)=> {
                var member = (Developer) subscriber.NotifiedUser;
                if (member.Seniority)
                {
                    subscriber.Notify(title, message);
                }
            });
        }
    }
}
