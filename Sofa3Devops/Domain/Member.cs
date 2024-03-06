using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;
using Sofa3Devops.NotificationStrategy;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public string Name { get; set; }
        public SprintStrategy SprintStrategy { get; set; }
        public List<CommentThread> PostedThreads { get; set; }
        public List<Response> PostedResponses { get; set; }
        public string EmailAddress { get; set; }
        public string SlackUserName { get; set; }
        //A member always has to be notifiable, hence the default value
        public INotificationAdapter WayToNotify { get; set; } = new EmailAdapter();


        public Member(string name, string emailAddress, string slackUserName) {
            Name = name;
            PostedResponses = new List<Response>();
            PostedThreads = new List<CommentThread>();
            EmailAddress = emailAddress;
            SlackUserName = slackUserName;
        }

        public void SetSprintStrategy(SprintStrategy strategy)
        {
            SprintStrategy = strategy;
        }

        public virtual Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return SprintStrategy.CreateSprint(start, end, name);
        }

        public virtual Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem)
        {
            return SprintStrategy.AddBacklogItem(sprint, backlogItem);
        }

        public void SetWayToNotify(INotificationAdapter wayToNotify)
        {
            this.WayToNotify = wayToNotify;
        }

        public virtual void StartSprint(Sprint sprint)
        {
            SprintStrategy.StartSprint(sprint);
        }

        public virtual void CancelSprint(Sprint sprint)
        {
            SprintStrategy.CancelSprint(sprint);
        }

        public void PickupBacklogItem(BacklogItem backlogItem)
        {
            backlogItem.AssignBacklogItem(this);
        }

        // Only relevant for testers and ReadyForTesting backlogitems.
        public virtual void ApproveItemForTesting(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to approve item for testing. Only testers are allowed to move.");
        }

        // Only relevant for testers and ReadyForTesting backlogitems.
        public virtual void DisapproveItemForTesting(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to disapprove item for testing. Only testers are allowed to move.");
        }

        public virtual void ApproveAndFinishItem(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to Finish the backlog item. Only lead developers are allowed to do that.");
        }

        public virtual void DisapproveTestedItem(BacklogItem item)
        {
            throw new UnauthorizedAccessException("Does not have authority to Disapprove the backlog item. Only lead developers are allowed to do that.");
        }
    }
}
