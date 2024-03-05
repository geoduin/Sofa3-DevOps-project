using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.Adapters
{
    public interface INotificationAdapter
    {
        public void SendNotification(string title, string message, DateTime dateOfWriting, Member recipient);
    }
}
