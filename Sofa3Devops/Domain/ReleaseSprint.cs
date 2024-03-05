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

        public override void NotifyAll()
        {
            this.NotificationStrategy.SendNotification($"Update about Sprint {Name}", $"The release for sprint {Name} has been canncelled", this.Subscribers);
        }
    }
}
