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
            throw new InvalidOperationException("Can't set item to todo from tested");
        }

        public void SetToFinished(BacklogItem item)
        {
            // Validate if all activities are done.
            if (item.HasAllTaskBeenCompleted())
            {
                item.SetBacklogState(new FinishedState());
                // Notify all members.
                item.NotifyAll($"Backlogitem: {item.Name}, has been completed.", "Backlogitem status is from Tested to Finised. Please resume your work.");
            }
            else
            {
                throw new InvalidOperationException("All tasks and subtasks need to be completed, before finishing this backlog item");
            }
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            item.SetBacklogState(new ReadyToTestingState());
            item.NotifyAll($"Backlogitem: {item.Name}, is moved back for more testing.", "The lead developer has stated that the current implementation does not fullfill the Definition of Done as written down. More testing is required.");
        }

        public void SetToTested(BacklogItem item)
        {
            // Logic needs to be implemented.
            throw new NotImplementedException();
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }
    }
}
