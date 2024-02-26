using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Observers
{
    public interface INotificationObserver
    {
        void AddSubscriber(Subscriber subscriber);
        void RemoveSubscriber(Subscriber subscriber);
        void NotifyAll();
    }
}
