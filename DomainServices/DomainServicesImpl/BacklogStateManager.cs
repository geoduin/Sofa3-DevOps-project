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

        public BacklogStateManager()
        {
            validator = null;
        }

        public void FinishTesting(Member member, BacklogItem item)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTested();
        }

        public void SetItemBackToTodo(Member member, BacklogItem item)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTodo();
        }

        public void SetItemForReadyTesting(Member member, BacklogItem item)
        {
            item.SetItemReadyForTesting();
        }

        public void FinishItem(Member member, BacklogItem item)
        {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);
            item.SetItemToFinished();
        }

        public void RejectTestedItem(Member member, BacklogItem item)
        {
            validator = new LeadDeveloperValidation();
            validator.HasPermission(member);
            item.SetItemReadyForTesting();
        }

        public void AcceptItemForTesting(Member member, BacklogItem item)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetToTesting();
        }
    }
}
