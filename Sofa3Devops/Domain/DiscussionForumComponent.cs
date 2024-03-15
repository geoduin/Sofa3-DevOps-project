using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.BacklogStates;

namespace Sofa3Devops.Domain
{
    public abstract class DiscussionForumComponent
    {

        private string Title { get; }
        private string Content { get; }
        private BacklogItem RelevantItem { get; }
        private Member Poster { get; }

        protected DiscussionForumComponent(string title, string content, BacklogItem relevantItem, Member poster)
        {
            if (CanBeCreated(relevantItem))
            {
                Title = title;
                Content = content;
                RelevantItem = relevantItem;
                Poster = poster;
            }
            else
            {
                throw new InvalidOperationException("Can't create discussion component when the backlog item or sprint is finished");
            }

        }

        private bool CanBeCreated(BacklogItem item)
        {
            return !item.State.GetType().Equals(typeof(FinishedState)) &&
                   !item.Sprint.State.GetType().Equals(typeof(SprintStates.FinishedState));
        }

        public abstract void AddComponent(DiscussionForumComponent component);
        public abstract void RemoveComponent(DiscussionForumComponent component);
        public abstract DiscussionForumComponent GetChild(int index);
        public abstract List<DiscussionForumComponent> GetChildren();
        public abstract DiscussionForumComponent GetParent();

    }
}
