using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using System.ComponentModel.DataAnnotations;


namespace Sofa3Devops.BacklogStates
{
    public class TestedState : IBacklogState
    {
        private IAuthValidationBehavior? validator { get; set; }

        public void SetDoing(BacklogItem item, Member member)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item, Member member)
        {
            throw new InvalidOperationException("Can't set item to todo from tested");
        }

        public void SetToFinished(BacklogItem item, Member member)
        {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);

            // Validate if all activities are done.
            if (item.HasAllTaskBeenCompleted())
            {
                item.Activities.ForEach(act => act.SetItemToFinished(member));
                item.SetBacklogState(new FinishedState());
            }
            else
            {
                throw new InvalidOperationException("All tasks and subtasks need to be completed, before finishing this backlog item");
            }
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);

            item.SetBacklogState(new ReadyToTestingState());
            item.Sprint!.SetNotificationStrategy(new TesterNotificationStrategy());
            item.NotifyAll($"Backlogitem: {item.Name}, is moved back for more testing.", "The lead developer has stated that the current implementation does not fullfill the Definition of Done as written down. More testing is required.");
        }

        public void SetToTested(BacklogItem item, Member member)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item, Member member)
        {
            throw new NotImplementedException();
        }
    }
}
