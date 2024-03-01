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
        private readonly DomainFactory domainFactory;

        public AuthorizedSprintStrategy(DomainFactory domainFactory) {
            this.domainFactory = domainFactory;
        } 

        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return domainFactory.CreateSprint(start, end, name);
        }
    }
}
