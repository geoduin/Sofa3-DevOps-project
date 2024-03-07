using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.Observers
{
    public class RegularSubscriber : Subscriber
    {
        public RegularSubscriber(Member NotifiedUser) : base(NotifiedUser)
        {
        }

        public override void Notify(string title, string message)
        {
            NotifiedUser.WayToNotify.SendNotification(title, message, DateTime.Now, this.NotifiedUser);
        }
    }
}
