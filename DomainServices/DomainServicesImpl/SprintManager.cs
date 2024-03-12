using DomainServices.DomainServicesIntf;
using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomainServicesImpl
{
    public class SprintManager: ISprintManager
    {
        private readonly AbstractSprintFactory abstractSprintFactory;
        private IAuthValidationBehavior? authValidator { get; set; }

        public SprintManager(AbstractSprintFactory abstractSprintFactory) {
            this.abstractSprintFactory = abstractSprintFactory;
            authValidator = null;
        }

        public virtual void StartSprint(Sprint sprint, Member member)
        {
            authValidator = new ScrumMasterPOValidation();
            authValidator.HasPermission(member);
            sprint.StartSprint();
        }

        public virtual void CancelSprint(Sprint sprint, Member member)
        {
            authValidator = new ScrumMasterPOValidation();
            authValidator.HasPermission(member);
            sprint.CancelSprint();
        }

        public virtual Sprint CreateSprint(DateTime start, DateTime end, string name, Member member)
        {
            authValidator = new ScrumMasterPOValidation();
            authValidator.HasPermission(member);
            return abstractSprintFactory.CreateSprint(start, end, name);
        }

        public virtual Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem, Member member)
        {
            authValidator = new ScrumMasterPOValidation();
            authValidator.HasPermission(member);
            sprint.AddBacklogItem(backlogItem);
            return sprint;
        }
    }
}
