using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;

namespace Sofa3Devops.BacklogStates
{
    public class DoingState : IBacklogState
    {
        public void SetDoing(BacklogItem item, Member member)
        {
            item.SetBacklogState(this);
            Console.WriteLine("Nothing happens");
        }

        public void SetToDo(BacklogItem item, Member member)
        {
            item.SetBacklogState(new TodoState());
        }

        public void SetToFinished(BacklogItem item, Member member)
        {
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
            var strat = item.Sprint!.NotificationStrategy;
            item.State = new ReadyToTestingState();
            // Set strategy to send to Testers.
            item.SetNotificationStrategy(new TesterNotificationStrategy());
            // Notifies all testers of backlog-item that are ready for testing.
            item.NotifyAll($"Update over {item.Name}", $"Backlog item {item.Name} has been updated to {item.State}");
            item.Sprint.SetNotificationStrategy(strat);
        }

        public void SetToTested(BacklogItem item, Member member)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item, Member member)
        {
            throw new InvalidOperationException();
        }
    }
}
