using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStrategies
{
    public class AuthorizedSprintStrategy : SprintStrategy
    {
        private readonly AbstractSprintFactory abstractSprintFactory;

        public AuthorizedSprintStrategy(AbstractSprintFactory factory) {
            this.abstractSprintFactory = factory;
        }

        public override Sprint AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            sprint.AddBacklogItem(item);
            return sprint;
        }

        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return abstractSprintFactory.CreateSprint(start, end, name);
        }

        public override Sprint StartSprint(Sprint sprint)
        {
            sprint.StartSprint();
            return sprint;
        }
    }
}
