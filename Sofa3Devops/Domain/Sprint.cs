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
        public ISprintState? State { get; set; }
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
            State = null;
            PublishingPipeline = null;
            BacklogItems = new List<BacklogItem>();
            Members = new List<Member>();
        }

        public void AddBacklogItem(BacklogItem item)
        {
            BacklogItems.Add(item);
        }

        public void ChangeSprint(DateTime newStart, DateTime newEnd, string newName)
        {
            if (State != null)
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
            bool leadTesterFlag = false;
            bool leadDeveloperFlag = false;
            
            var testers = Members.FindAll(x => x.GetType() == typeof(Tester));
            var leadDevs = Members.FindAll(x => x.GetType() == typeof(Developer));

            leadTesterFlag = testers.Count > 0;
            leadDeveloperFlag = ContainsLeadDeveloper(leadDevs);
            // Check if at least one scrum-master is assigned
            if (AssignScrumMaster == null)
            {
                throw new InvalidOperationException("At least one scrummaster must be assigned to a sprint");
            }
            // Check if at least one tester is assigned
            // Check if at least one lead developer is assigned.
            else if (!leadTesterFlag || !leadDeveloperFlag)
            {
                throw new InvalidOperationException("At least one tester and developer must be added, before starting a sprint");
            }
            // Start sprint
            Console.WriteLine("Sprint can no be started");
            // Perhaps notify all parties.
        }

        private bool ContainsLeadDeveloper(List<Member> list)
        {
            var castedList = list.Cast<Developer>().ToList();
            var result = castedList.FindAll(x => x.Seniority);
            return result.Count > 0;
        }
    }
}
