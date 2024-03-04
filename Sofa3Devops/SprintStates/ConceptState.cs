using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStates
{
    public class ConceptState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            throw new InvalidOperationException("Cannot cancel a sprint, without starting one.");
        }

        public void SetToFinished(Sprint sprint)
        {
            throw new InvalidOperationException("Cannot finish a sprint, without starting one.");
        }

        public void SetToOngoing(Sprint sprint)
        {
            var (devs, testers) = returnSeperatedLists(sprint.Members);

            if (sprint.AssignScrumMaster != null && testers.Any() && ContainsLeadDeveloper(devs))
            {
                sprint.SetSprintState(new OngoingState());
            }
            else
            {
                throw new InvalidOperationException("At least one tester, scrummaster and developer must be added, before starting a sprint");
            }
        }

        private (List<Member> devs, List<Member> testers) returnSeperatedLists(List<Member> members)
        {
            List<Member> testUserList = SeperateAndValidate(members, typeof(Tester));
            List<Member> leadDevList = SeperateAndValidate(members, typeof(Developer));

            return (leadDevList, testUserList);
        }
     
        private List<Member> SeperateAndValidate(List<Member> memberList, Type type2)
        {
            return memberList.FindAll(x => x.GetType() == type2);
        }

        private bool ContainsLeadDeveloper(List<Member> list)
        {
            return list.Cast<Developer>().Any(x => x.Seniority);
        }
    }
}
