namespace Sofa3Devops.Domain
{
    public class Activity : BacklogItem
    {

        public BacklogItem AssignedBacklogItem { get; set; }

        public Activity(string name, string description, BacklogItem backlogItem) : base(name, description)
        {
            AssignedBacklogItem = backlogItem;
        }

        public override void AssignBacklogItem(Member member)
        {
            base.AssignBacklogItem(member);
            // Set state of parent backlog item to doing
            SetParentStateToDoing();
        }

        public void SetParentStateToDoing()
        {
            AssignedBacklogItem.SetToDoing();
        }
    }
}