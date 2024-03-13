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
        public DiscussionComment(string title, string content, BacklogItem relevantItem, Member poster, DiscussionForumComponent parent) : base(title, content, relevantItem, poster)
        {
            this.Parent = parent;
        }

        public override void AddComponent(DiscussionForumComponent component)
        {
            //TODO: Discuss wether we should be able to add comments to comments
            throw new NotImplementedException();
        }

        public override DiscussionForumComponent GetChild(int index)
        {
            //TODO: Discuss wether we should be able to add comments to comments
            throw new NotImplementedException();
        }

        public override List<DiscussionForumComponent> GetChildren()
        {
            //TODO: Discuss wether we should be able to add comments to comments
            throw new NotImplementedException();
        }

        public override DiscussionForumComponent GetParent()
        {
            return Parent;
        }

        public override void RemoveComponent(DiscussionForumComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
