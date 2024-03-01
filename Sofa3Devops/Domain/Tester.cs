using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class Tester : Member
    {
        public Tester(string name) : base(name)
        {

        }

        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            SetSprintStrategy(new NonAuthorizedSprintStrategy());
            return base.CreateSprint(start, end, name);
        }
    }
}
