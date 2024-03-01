namespace Sofa3Devops.Domain
{
    public class Activity : BacklogItem
    {

        public BacklogItem AssignedBacklogItem { get; set; }

        public Activity(string name, string description, BacklogItem backlogItem) : base(name, description)
        {
            AssignedBacklogItem = backlogItem;
        }
    }
}