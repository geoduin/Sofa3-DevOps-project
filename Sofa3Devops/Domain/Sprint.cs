using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public abstract class Sprint
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Name { get; private set; }
        public ISprintState State { get; set; }
        public SprintReport? SprintReport { get; set; }
        public Pipeline? PublishingPipeline { get; set; }
        public Member AssignScrumMaster {  get; set; }
        public List<BacklogItem> BacklogItems { get; set; }
        public List<Member> Members { get; set; }
        public ISprintNotificationStrategy Notification { get; set; }


        public Sprint(DateTime startDate, DateTime endDate, string name)
        {
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
            State = new ConceptState();
            PublishingPipeline = null;
            BacklogItems = new List<BacklogItem>();
            Members = new List<Member>();
        }

        public void SetSprintState(ISprintState sprintState)
        {
            State = sprintState;
        }

        public void AddBacklogItem(BacklogItem item)
        {
            BacklogItems.Add(item);
        }

        public void ChangeSprint(DateTime newStart, DateTime newEnd, string newName)
        {
            if (State.GetType() != typeof(ConceptState))
            {
                throw new InvalidOperationException("Backlog items cannot be changed on ongoing sprint");
            }

            // Apply change.
            StartDate = newStart;
            EndDate = newEnd;
            Name = newName;
        }

        public void AssignMembersToSprint(Member member)
        {
            if(member.GetType() == typeof(ScrumMaster))
            {
                AssignScrumMasterToSprint(member);
            }
            else
            {
                Members.Add(member);
            }
        }

        private void AssignScrumMasterToSprint(Member member)
        {
            if (AssignScrumMaster == null)
            {
                AssignScrumMaster = member;
            }
            else
            {
                throw new InvalidOperationException("This sprint has already been assigned to a scrummaster");
            }
        }
    
        public void StartSprint()
        {
            State.SetToOngoing(this);
        }

        public void CancelSprint()
        {
            State.SetToCanceled(this);
        }
    }
}
