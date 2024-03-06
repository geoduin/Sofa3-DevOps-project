using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.NotificationStrategy;

namespace Sofa3Devops.BacklogStates
{
    public class TestedState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item)
        {
            item.State = new TodoState();
        }

        public void SetToFinished(BacklogItem item)
        {
            item.State = new FinishedState();
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            item.State = new ReadyToTestingState();
            foreach (var activity in item.Activities)
            {
                activity.State = new ReadyToTestingState();
            }
            var strat = item.Sprint.NotificationStrategy;
            item.Sprint.SetNotificationStrategy(new TesterNotificationStrategy());
            item.Sprint.NotifyAll($"Update for backlog item {item.Name}", $"Backlog item {item.Name} has been set to {item.State}");
            item.Sprint.SetNotificationStrategy(strat);
        }

        public void SetToTested(BacklogItem item)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }
    }
}
