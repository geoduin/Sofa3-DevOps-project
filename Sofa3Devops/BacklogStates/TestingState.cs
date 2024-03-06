using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.NotificationStrategy;

namespace Sofa3Devops.BacklogStates
{
    public class TestingState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item)
        {
            item.State = new TodoState();
            var strat = item.NotificationStrategy;
            item.SetNotificationStrategy(new ScrumMasterStrategy());
            item.NotifyAll($"Update for backlog item {item.Name}", $"{item.Name} has been set to {item.State}");
            item.SetNotificationStrategy(strat);
        }

        public void SetToFinished(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToTested(BacklogItem item)
        {
            var strat = item.NotificationStrategy;
            item.Sprint.SetNotificationStrategy(new ScrumMasterStrategy());
            item.State = new TestedState();
            item.NotifyAll($"Update for backlog item {item.Name}", $"{item.Name} has been set to {item.State}");
            item.Sprint.SetNotificationStrategy(strat);
        }

        public void SetToTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }
    }
}
