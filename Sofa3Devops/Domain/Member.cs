using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public string Name { get; set; }
        public SprintStrategy SprintStrategy { get; set; }
        public List<CommentThread> PostedThreads { get; set; }
        public List<Response> PostedResponses { get; set; }

        public Member(string name) {
            Name = name;
            PostedResponses = new List<Response>();
            PostedThreads = new List<CommentThread>();
        }

        public void SetSprintStrategy(SprintStrategy strategy)
        {
            SprintStrategy = strategy;
        }

        public virtual Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return SprintStrategy.CreateSprint(start, end, name);
        }

        public virtual Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem)
        {
            return SprintStrategy.AddBacklogItem(sprint, backlogItem);
        }

        public virtual void StartSprint(Sprint sprint)
        {
            SprintStrategy.StartSprint(sprint);
        }
    }
}
