using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class ScrumMaster : Member
    {

        public ScrumMaster(string name, string emailAddress, string slackUserName) : base(name, emailAddress, slackUserName)
        {
        }

        public override string ToString()
        {
            return "Scrum-master";
        }
    }
}
