using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomainServicesIntf
{
    public interface IBacklogStateManager
    {
        // Ready for Testing --> Tested
        public void FinishTesting(Member member, BacklogItem item);

        // Ready for Testing --> Todo
        public void SetItemBackToTodo(Member member, BacklogItem item);

        // Doing --> Ready for testing
        public void SetItemForReadyTesting(Member member, BacklogItem item);

        // Tested --> Finished
        public void FinishItem(Member member, BacklogItem item);

        // Tested --> Ready for Testing 
        public void RejectTestedItem(Member member, BacklogItem item);

        // Ready for Testing --> Testing
        public void AcceptItemForTesting(Member member, BacklogItem item);
    }
}
