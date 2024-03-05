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
        public Tester(string name, string emailAddress, string slackUserName) : base(name, emailAddress, slackUserName)
        {
        }

        public override void ApproveItemForTesting(BacklogItem item)
        {
            item.SetToTesting();
        }

        public override void DisapproveItemForTesting(BacklogItem item)
        {
            item.SetToTodo();
        }
    }
}
