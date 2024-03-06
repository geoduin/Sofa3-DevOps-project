using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStrategies
{
    public class NonAuthorizedSprintStrategy : SprintStrategy
    {
        public override Sprint AddBacklogItem(Sprint sprint, BacklogItem item)
        {
            throw DefaultAuthorisationError();
        }

        public override Sprint CancelSprint(Sprint sprint)
        {
            throw DefaultAuthorisationError();
        }

        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            throw DefaultAuthorisationError();
        }

        public override Sprint StartSprint(Sprint sprint)
        {
            throw DefaultAuthorisationError();
        }
    }
}
