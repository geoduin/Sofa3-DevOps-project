using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;


namespace Sofa3Devops.BacklogStates
{
    public class TestingState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
        }

        public void SetToDo(BacklogItem item)
        {

            item.SetBacklogState(new TodoState());
            item.Sprint!.SetNotificationStrategy(new ScrumMasterNotificationStrategy());
            item.NotifyAll($"Backlog item: {item.Name} has been rejected.", "This backlog-item needs be implemented better.");
        }

        public void SetToFinished(BacklogItem item)
        {
        }

        public void SetToReadyTesting(BacklogItem item)
        {
        }

        public void SetToTested(BacklogItem item)
        {
            var strat = item.NotificationStrategy;
            item.Activities.ForEach(activities => activities.SetBacklogState(new TestedState()));
            item.Sprint!.SetNotificationStrategy(new LeadDeveloperNotificationStrategy());
            item.SetBacklogState(new TestedState());
        }

        public void SetToTesting(BacklogItem item)
        {
        }
    }
}
