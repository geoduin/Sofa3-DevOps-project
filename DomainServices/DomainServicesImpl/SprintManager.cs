using DomainServices.DomainServicesIntf;
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
        private readonly List<Type> authorizedArray;

        public SprintManager(AbstractSprintFactory abstractSprintFactory) {
            this.abstractSprintFactory = abstractSprintFactory;
            authorizedArray = new List<Type>() {
                typeof(ProductOwner), typeof(ScrumMaster)
            };
        }

        public virtual void StartSprint(Sprint sprint, Member member)
        {
            if (performAction(member))
            {
                sprint.StartSprint();
                return;
            }
            throw AuthorizationException();
        }

        public virtual void CancelSprint(Sprint sprint, Member member)
        {
            if(performAction(member))
            {
                sprint.CancelSprint();
                return;
            }
            throw AuthorizationException();
        }

        public virtual Sprint CreateSprint(DateTime start, DateTime end, string name, Member member)
        {
            if(performAction(member))
            {
                return abstractSprintFactory.CreateSprint(start, end, name);
            }
            throw AuthorizationException();
        }

        public virtual Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem, Member member)
        {
            if (performAction(member))
            {
                sprint.AddBacklogItem(backlogItem);
                return sprint;
            }
            throw AuthorizationException();
        }

        private UnauthorizedAccessException AuthorizationException()
        {
            return new UnauthorizedAccessException("Does not have the right authorization to perform this action.");
        }

        private bool performAction(Member member) {
            return authorizedArray.Contains(member.GetType());
        }
    }
}
