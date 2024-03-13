using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class DevelopmentSprint : Sprint
    {
        public DevelopmentSprint(DateTime startDate, DateTime endDate, string name) : base(startDate, endDate, name)
        {
        }

        public override void NotifyAll(string title, string message)
        {
            NotificationStrategy.SendNotification(title, message, Subscribers);
        }

        public override void EndSprint(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
