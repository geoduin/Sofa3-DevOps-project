using DomainServices.DomainServicesIntf;
using Sofa3Devops.AuthorisationStrategy;
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
        private IAuthValidationBehavior? validator { get; set; }

        public BacklogStateManager() {
            validator = null;
        }

        public void FinishTesting(Member member, BacklogItem item) {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTested(member);
        }
        
        public void SetItemBackToTodo(Member member, BacklogItem item) {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTodo(member);
        }
        
        public void SetItemForReadyTesting(Member member, BacklogItem item) {
            item.SetItemReadyForTesting(member);
        }
        
        public void FinishItem(Member member, BacklogItem item) {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);
            item.SetItemToFinished(member);
        }
        
        public void RejectTestedItem(Member member, BacklogItem item) {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);
            item.SetItemReadyForTesting(member);
        }
        
                item.SetItemReadyForTesting();
            }
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTesting(member);
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
