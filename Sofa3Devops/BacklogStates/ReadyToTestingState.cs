using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.BacklogStates
{
    public class ReadyToTestingState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item)
        {
            item.Sprint.SetNotificationStrategy(new ScrumMasterNotificationStrategy());
            
            item.SetBacklogState(new TodoState());
            item.NotifyAll($"Backlog-item: {item.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {item.State.GetType().Name}");
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
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item)
        {
            item.SetBacklogState(new TestingState());
        }
    }
}
