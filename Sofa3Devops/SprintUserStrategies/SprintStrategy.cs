using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStrategies
{
    public abstract class SprintStrategy
    {
        protected UnauthorizedAccessException DefaultAuthorisationError()
        {
            return new UnauthorizedAccessException("Does not have the right authorization to perform this action.");
        }

        public abstract Sprint CreateSprint(DateTime start, DateTime end, string name);
        public abstract Sprint AddBacklogItem(Sprint sprint, BacklogItem item);
    }
}
