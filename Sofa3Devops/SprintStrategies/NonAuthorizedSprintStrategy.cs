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
        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            throw new UnauthorizedAccessException("Does not have the right authorization to perform this action.");
        }
    }
}
