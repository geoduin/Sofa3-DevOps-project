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


        public Developer(string name, string emailAddress, string slackUserName) : base(name, emailAddress,
            slackUserName)
        {
        }


        public void SetLeadDeveloper()
        {
            Seniority = true;
        }

        public override string ToString()
        {
            return Seniority ? "Lead developer": "Developer";
        }
    }
}
