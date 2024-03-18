using Sofa3Devops.Adapters;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public string Name { get; set; }
        //public SprintStrategy? SprintStrategy { get; set; }
        public List<AbstractDiscussionComponent> PostedDiscussionForumComponents { get; set; }
        public string EmailAddress { get; set; }
        public string SlackUserName { get; set; }
        //A member always has to be notifiable, hence the default value
        public INotificationAdapter WayToNotify { get; set; } = new EmailAdapter();


        public Member(string name, string emailAddress, string slackUserName) {
            Name = name;
            PostedDiscussionForumComponents = new List<AbstractDiscussionComponent>();
            EmailAddress = emailAddress;
            SlackUserName = slackUserName;
        }

        public void SetWayToNotify(INotificationAdapter wayToNotify)
        {
            this.WayToNotify = wayToNotify;
        }
    }
}
