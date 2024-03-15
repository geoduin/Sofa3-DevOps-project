using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public class DiscussionComment : DiscussionForumComponent
    {
        public DiscussionForumComponent Parent { get; set; }
        private List<DiscussionForumComponent> Replies;

        public DiscussionComment(string title, string content, BacklogItem relevantItem, Member poster) : base(title, content, relevantItem, poster)
        {
            this.Replies = new List<DiscussionForumComponent>();
        }

        private bool SprintStateIsFinished()
        {
            return this.RelevantItem.Sprint.State.GetType().Equals(typeof(FinishedState));
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
            if (component.GetType().Equals(typeof(DiscussionComment)) && !SprintStateIsFinished())
            {
                this.Replies.Add(component);
                component.SetParent(this);
            }
            else
            {
                throw new InvalidOperationException("Can't add thread to a comment or add comments when sprint is finished");
            }
        }

        public override void SetParent(DiscussionComment component)
        {
            this.Parent = component;
        }
    }
}
