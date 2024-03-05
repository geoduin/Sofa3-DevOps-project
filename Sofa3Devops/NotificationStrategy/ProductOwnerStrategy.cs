﻿using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.NotificationStrategy
{
    internal class ProductOwnerStrategy : INotificationStrategy
    {
        public void SendNotification(string title, string message, Dictionary<Type, List<Subscriber>> subscribers)
        {
            var productOwners = subscribers[typeof(ProductOwner)];
            foreach (var productOwner in productOwners)
            {
                productOwner.Notify(title, message);
            }
        }
    }
}
