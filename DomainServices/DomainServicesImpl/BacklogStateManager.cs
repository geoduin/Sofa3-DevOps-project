using DomainServices.DomainServicesIntf;
using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public void AcceptItemForTesting(Member member, BacklogItem item)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTesting(member); 
        }
    }
}
