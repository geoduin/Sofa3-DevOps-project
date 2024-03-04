using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class ProductOwner : Member
    {
        public ProductOwner(string emailAddress, string slackUserName) : base(emailAddress, slackUserName)
        {
        }
        public ProductOwner(string name) :base(name) { }
    }
}
