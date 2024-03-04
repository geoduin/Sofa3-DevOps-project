using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class Developer : Member
    {
        public bool Seniority { get; set; } = false;

        public Developer(string name): base(name) {
            
        }

        public void SetLeadDeveloper()
        {
            Seniority = true;
        }
    }
}
