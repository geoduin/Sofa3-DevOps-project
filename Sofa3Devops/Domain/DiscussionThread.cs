using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.BacklogStates;
using FinishedState = Sofa3Devops.SprintStates.FinishedState;

namespace Sofa3Devops.Domain
{
    public class DiscussionThread : DiscussionForumComponent
    {

       
        private List<DiscussionForumComponent> Comments;

        public DiscussionThread(string title, string content, BacklogItem relevantItem, Member poster) : base(title, content, relevantItem, poster)
        {
            if (!SprintStateIsFinished())
            {
                Comments = new List<DiscussionForumComponent>();
            }
            else
            {
                throw new InvalidOperationException("can't create thread for a backlog item in a finished sprint");
            }
        }

        private bool SprintStateIsFinished()
        {
            return this.RelevantItem.Sprint.State.GetType().Equals(typeof(FinishedState));
        }

        public override void AddComponent(DiscussionForumComponent component)
        {
            if (component.GetType().Equals(typeof(DiscussionComment)) && !SprintStateIsFinished())
            {
                this.Comments.Add(component);
                this.RelevantItem.Threads.Add(this);
            }
            else
            {
                throw new InvalidOperationException("Can't add thread to a thread or comment to a finished sprint");
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
            throw new InvalidOperationException("Thread does not have a parent node");
        }

        public override void RemoveComponent(DiscussionForumComponent component)
        {
            this.Comments.Remove(component);
        }

        public override void SetParent(DiscussionComment component)
        {
            throw new InvalidOperationException();
        }
    }
}
