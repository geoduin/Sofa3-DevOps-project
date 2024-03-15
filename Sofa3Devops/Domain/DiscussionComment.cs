using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    internal class DiscussionComment : DiscussionForumComponent
    {
        public DiscussionForumComponent Parent { get; }
        private List<DiscussionForumComponent> Replies;

        public DiscussionComment(string title, string content, BacklogItem relevantItem, Member poster, DiscussionForumComponent parent) : base(title, content, relevantItem, poster)
        {
            this.Parent = parent;
        }


        public override DiscussionForumComponent GetChild(int index)
        {
            return this.Replies[index];
        }

        public override List<DiscussionForumComponent> GetChildren()
        {
            return this.Replies;
        }

        public override DiscussionForumComponent GetParent()
        {
            return Parent;
        }

        public override void RemoveComponent(DiscussionForumComponent component)
        {
            this.Replies.Remove(component);
        }

        public override void AddComponent(DiscussionForumComponent component)
        {
            if (component.GetType().Equals(typeof(DiscussionComment)))
            {
                this.Replies.Add(component);
            }
            else
            {
                throw new InvalidOperationException("Can't add thread to a thread");
            }
        }
    }
}
