using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class DiscussionThread : DiscussionForumComponent
    {

       
        private List<DiscussionForumComponent> Comments;

        public DiscussionThread(string title, string content, BacklogItem relevantItem, Member poster) : base(title, content, relevantItem, poster)
        {
            Comments = new List<DiscussionForumComponent>();
        }

        public override void AddComponent(DiscussionForumComponent component)
        {
            if (component.GetType().Equals(typeof(DiscussionComment)))
            {
                this.Comments.Add(component);
            }
            else
            {
                throw new InvalidOperationException("Can't add thread to a thread");
            }
        }

        public override DiscussionForumComponent GetChild(int index)
        {
            return Comments[index];
        }

        public override List<DiscussionForumComponent> GetChildren()
        {
            return Comments;
        }

        public override DiscussionForumComponent GetParent()
        {
            throw new InvalidOperationException();
        }

        public override void RemoveComponent(DiscussionForumComponent component)
        {
            this.Comments.Remove(component);
        }
    }
}
