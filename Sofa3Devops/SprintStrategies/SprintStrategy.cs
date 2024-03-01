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
        public abstract Sprint CreateSprint(DateTime start, DateTime end, string name);
    }
}
