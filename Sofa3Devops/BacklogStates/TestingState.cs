using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using System.ComponentModel.DataAnnotations;


namespace Sofa3Devops.BacklogStates
{
    public class TestingState : IBacklogState
    {

        private IAuthValidationBehavior? validator { get; set; }

        public void SetDoing(BacklogItem item, Member member)
        {
        }

        public void SetToDo(BacklogItem item, Member member)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);

            item.SetBacklogState(new TodoState());
            item.Sprint!.SetNotificationStrategy(new ScrumMasterNotificationStrategy());
            item.NotifyAll($"Backlog item: {item.Name} has been rejected.", "This backlog-item needs be implemented better.");
        }

        public void SetToFinished(BacklogItem item, Member member)
        {
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
        }

        public void SetToTested(BacklogItem item, Member member)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);

            item.Activities.ForEach(activities => activities.SetBacklogState(new TestedState()));
            item.Sprint!.SetNotificationStrategy(new LeadDeveloperNotificationStrategy());
            item.SetBacklogState(new TestedState());
        }

        public void SetToTesting(BacklogItem item, Member member)
        {
        }
    }
}
