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
            item.SetBacklogState(new TodoState());
            // Send notification to scrum master.
            item.Sprint.SetNotificationStrategy(new ScrumMasterNotificationStrategy());
            item.NotifyAll($"Backlog-item: {item.Name} has been rejected for testing.", "This backlog-item is rejected by our testers. The scrum-master will reprehend the responsible developer for his implementation.");
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
            throw new NotImplementedException();
        }

        public void SetToTesting(BacklogItem item)
        {
            item.SetBacklogState(new TestingState());
        }
    }
}
