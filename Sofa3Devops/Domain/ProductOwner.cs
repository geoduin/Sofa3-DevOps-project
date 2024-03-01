using Sofa3Devops.Factories;
using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class ProductOwner : Member
    {
        public ProductOwner(string name) :base(name) { }

        public override Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return base.CreateSprint(start, end, name);
        }
    }
}
