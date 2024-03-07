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

        // ReadyForTesting -> Testing
        public override void ApproveItemForTesting(BacklogItem item)
        {
            // When state is in ReadyForTesting -> Testing, this method will work without exceptions.
            // Otherwise, exception will be thrown.
            item.SetToTesting();
        }


        public override void DisapproveItemForTesting(BacklogItem item)
        {
            // When state is in ReadyForTesting -> Tested, this method will work without exceptions.
            // Otherwise, exception will be thrown.
            item.SetToTodo();
        }

        // Testing -> Tested
        public override void SetItemFromTestingToTested(BacklogItem item)
        {
            item.SetToTested();
        }

        // Testing -> Todo
        public override void SetItemFromTestingBackToTodo(BacklogItem item)
        {
            item.SetToTodo();
        }

    }
}
