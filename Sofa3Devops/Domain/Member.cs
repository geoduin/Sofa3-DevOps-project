using Sofa3Devops.SprintStrategies;
using Sofa3Devops.Adapters;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public string Name { get; set; }
        public SprintStrategy? SprintStrategy { get; set; }
        public List<DiscussionForumComponent> PostedDiscussionForumComponents { get; set; }
        public string EmailAddress { get; set; }
        public string SlackUserName { get; set; }
        //A member always has to be notifiable, hence the default value
        public INotificationAdapter WayToNotify { get; set; } = new EmailAdapter();


        public Member(string name, string emailAddress, string slackUserName) {
            Name = name;
            PostedDiscussionForumComponents = new List<DiscussionForumComponent>();
            EmailAddress = emailAddress;
            SlackUserName = slackUserName;
            SprintStrategy = null;
        }

        public void SetSprintStrategy(SprintStrategy strategy)
        {
            SprintStrategy = strategy;
        }

        public virtual Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return SprintStrategy!.CreateSprint(start, end, name);
        }

        public virtual Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem)
        {
            return SprintStrategy!.AddBacklogItem(sprint, backlogItem);
        }

        public void SetWayToNotify(INotificationAdapter wayToNotify)
        {
            this.WayToNotify = wayToNotify;
        }

        public virtual void StartSprint(Sprint sprint)
        {
            SprintStrategy!.StartSprint(sprint);
        }

        public virtual void CancelSprint(Sprint sprint)
        {
            SprintStrategy!.CancelSprint(sprint);
        }

        // Doing ready testing.
        public void SetItemForReadyTesting(BacklogItem backlogItem)
        {
            backlogItem.SetItemReadyForTesting();
        }

        // 'to do' -> Doing
        public void PickupBacklogItem(BacklogItem backlogItem)
        {
            backlogItem.AssignBacklogItem(this);
        }

        // Only relevant for testers and ReadyForTesting backlogitems.
        // ReadyForTesting -> Testing
        public virtual void ApproveItemForTesting(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to approve item for testing. Only testers are allowed to move.");
        }

        // Only relevant for testers and ReadyForTesting backlogitems.
        // ReadyForTesting -> Testing
        public virtual void DisapproveItemForTesting(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to disapprove item for testing. Only testers are allowed to move.");
        }

        // Testing -> Tested
        public virtual void SetItemFromTestingToTested(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Non-testers are not allowed to move this item to tested.");
        }

        // Testing -> 'to do'
        public virtual void SetItemFromTestingBackToTodo(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Non-testers are not allowed to move this item to tested.");
        }

        // Tested -> Finished
        public virtual void ApproveAndFinishItem(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to Finish the backlog item. Only lead developers are allowed to do that.");
        }

        // Tested -> ReadyForTesting
        public virtual void DisapproveTestedItem(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to disapprove the backlog item. Only lead developers are allowed to do that.");
        }
    
        
    }
}
