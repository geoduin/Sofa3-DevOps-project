using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.BacklogStates;
using FinishedState = Sofa3Devops.SprintStates.FinishedState;

namespace Sofa3Devops.Domain
{
    public class DiscussionThread : AbstractDiscussionComponent
    {

       

        public DiscussionThread(string title, string content, BacklogItem relevantItem, Member poster) : base(title, content, relevantItem, poster)
        {
            if (!SprintStateIsFinished())
            {
                this.RelevantItem.Threads.Add(this);
                this.Poster.PostedDiscussionForumComponents.Add(this);

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

        public override void AddComponent(AbstractDiscussionComponent component)
        {
            if (component.GetType().Equals(typeof(DiscussionComment)) && !SprintStateIsFinished())
            {
                this.Children.Add(component);
                component.Poster.PostedDiscussionForumComponents.Add(component);
                this.NotifyAll($"New Comment on thread {this.Title}", $"User {component.Poster.Name} Has left a comment on the {this.Title} thread!");
            }
            else
            {
                throw new InvalidOperationException("Can't add thread to a thread or comment to a finished sprint");
            }
        }

        public override AbstractDiscussionComponent GetParent()
        {
            throw new InvalidOperationException("Thread does not have a parent node");
        }

        public override void RemoveComponent(AbstractDiscussionComponent component)
        {
            this.Children.Remove(component);
        }

        public override void SetParent(DiscussionComment component)
        {
            throw new InvalidOperationException();
        }
    }
}
