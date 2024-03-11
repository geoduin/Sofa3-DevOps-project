using DomainServices.DomainServicesIntf;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomainServicesImpl
{
    public class BacklogStateManager: IBacklogStateManager
    {
        public void FinishTesting(Member member, BacklogItem item) {
            if (IsTester(member))
            {
                item.SetToTested();
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to set item to Tested. Only testers are allowed to move backlog-item to Tested.");
            }
        }
        
        public void SetItemBackToTodo(Member member, BacklogItem item) {
            if (IsTester(member))
            {
                item.SetToTodo();
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to set item to Todo. Only testers are allowed to move backlog-item to Todo.");
            }
        }
        
        public void SetItemForReadyTesting(Member member, BacklogItem item) {
            item.SetItemReadyForTesting();
        }
        
        public void FinishItem(Member member, BacklogItem item) {
            if (IsLeadDeveloper(member))
            {
                item.SetItemToFinished();
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to finish a Backlog-item. This is reserved for lead-developers.");
            }
        }
        
        public void RejectTestedItem(Member member, BacklogItem item) {
            if (IsLeadDeveloper(member))
            {
                item.SetItemReadyForTesting();
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to reject a Backlog-item and put it back for testing. This is reserved for lead-developers.");
            }
        }
        
        public void AcceptItemForTesting(Member member, BacklogItem item)
        {
            if(IsTester(member))
            {
                item.SetToTesting();
            }
            else
            {
                throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to set item to testing. Only testers are allowed to move backlog-item to Testing.");
            }
        }

        private bool IsTester(Member member) { 
            return member.GetType() == typeof(Tester); 
        }

        private bool IsLeadDeveloper(Member member)
        {
            if (member.GetType() == typeof(Developer))
            {
                var dev = (Developer)member;
                return dev.Seniority;
            }
            else
            {
                return false;
            }
        }

    }
}
