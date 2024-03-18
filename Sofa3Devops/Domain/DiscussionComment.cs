using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public class DiscussionComment : AbstractDiscussionComponent
    {
        public AbstractDiscussionComponent Parent { get; set; }

        public DiscussionComment(string title, string content, BacklogItem relevantItem, Member poster) : base(title, content, relevantItem, poster)
        {
        }

        private bool SprintStateIsFinished()
        {
            return this.RelevantItem.Sprint.State.GetType().Equals(typeof(FinishedState));
        }


        public override AbstractDiscussionComponent GetParent()
        {
            return Parent;
        }

        public override void RemoveComponent(AbstractDiscussionComponent component)
        {
            this.Children.Remove(component);
        }

        public override void AddComponent(AbstractDiscussionComponent component)
        {
            if (component.GetType().Equals(typeof(DiscussionComment)) && !SprintStateIsFinished())
            {
                this.Children.Add(component);
                component.SetParent(this);
                component.Poster.PostedDiscussionForumComponents.Add(component);
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
