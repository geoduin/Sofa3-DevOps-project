using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class Developer : Member
    {
        public Developer(string emailAddress, string slackUserName) : base(emailAddress, slackUserName)
        {
        }
    }
}
