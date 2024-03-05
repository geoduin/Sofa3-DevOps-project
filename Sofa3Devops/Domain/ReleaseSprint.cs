using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class ReleaseSprint : Sprint
    {
        public ReleaseSprint(DateTime startDate, DateTime endDate, string name) : base(startDate, endDate, name)
        {
        }

        public override void NotifyAll(string title, string message)
        {
            this.NotificationStrategy.SendNotification(title, message, this.Subscribers);
        }
    }
}
