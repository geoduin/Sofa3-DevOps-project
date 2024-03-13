using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Title = title;
            Content = content;
            RelevantItem = relevantItem;
            Poster = poster;
        }

        public abstract void AddComponent(DiscussionForumComponent component);
        public abstract void RemoveComponent(DiscussionForumComponent component);
        public abstract DiscussionForumComponent GetChild(int index);
        public abstract List<DiscussionForumComponent> GetChildren();
        public abstract DiscussionForumComponent GetParent();

    }
}
