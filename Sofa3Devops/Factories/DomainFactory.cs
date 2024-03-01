using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Factories
{
    public class DomainFactory
    {
        public Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return new Sprint(start, end, name);
        }
    }
}
