﻿using Sofa3Devops.Adapters;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Observers;

namespace Sofa3Devops.NotificationStrategy
{
    public class TesterNotificationStrategy : INotificationStrategy
    {
        private INotificationAdapter notificationHandler;

        public TesterNotificationStrategy()
        {
        }

        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers)
        {
            var testers = subscribers[typeof(Tester)];
            foreach (var tester in testers)
            {
                tester.Notify(title, message);
            }
            
        }

    }
}
    