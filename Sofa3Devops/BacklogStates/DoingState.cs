using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sofa3Devops.NotificationStrategy;

namespace Sofa3Devops.BacklogStates
{
    public class DoingState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToFinished(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            var strat = item.Sprint.NotificationStrategy;
            item.State = new ReadyToTestingState();
            foreach (var activity in item.Activities)
            {
                activity.State = new ReadyToTestingState();
            }
            item.Sprint.SetNotificationStrategy(new TesterNotificationStrategy()); 
            item.NotifyAll($"Update over {item.Name}", $"Backlog item {item.Name} has been updated to {item.State}");
            item.Sprint.SetNotificationStrategy(strat);
        }

        public void SetToTested(BacklogItem item)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item)
        {
            throw new InvalidOperationException();
        }
    }
}
