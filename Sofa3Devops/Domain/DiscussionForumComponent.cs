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

        public string Title { get; }
        public string Content { get; }
        public BacklogItem RelevantItem { get; }
        public Member Poster { get; }

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
        public abstract void SetParent(DiscussionComment component);

    }
}
