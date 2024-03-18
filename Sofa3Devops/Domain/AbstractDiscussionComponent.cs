using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.BacklogStates;

namespace Sofa3Devops.Domain
{
    public abstract class AbstractDiscussionComponent
    {

        public string Title { get; set;  }
        public string Content { get; set;  }
        public BacklogItem RelevantItem { get; }
        public Member Poster { get; }
        public List<AbstractDiscussionComponent> Children { get; }

        protected AbstractDiscussionComponent(string title, string content, BacklogItem relevantItem, Member poster)
        {
                Title = title;
                Content = content;
                RelevantItem = relevantItem;
                Poster = poster;
                this.Children = new List<AbstractDiscussionComponent>();
        }


        public abstract void AddComponent(AbstractDiscussionComponent component);
        public abstract void RemoveComponent(AbstractDiscussionComponent component);
        public abstract AbstractDiscussionComponent GetParent();
        public abstract void SetParent(DiscussionComment component);

    }
}
