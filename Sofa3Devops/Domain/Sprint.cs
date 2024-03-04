using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public class Sprint
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public ISprintState State { get; set; }
        public SprintReport SprintReport { get; set; }
        public Pipeline PublishingPipeline { get; set; }
        public List<BacklogItem> BacklogItems { get; set; }
        public List<Member> Members { get; set; }
        public ISprintNotificationStrategy Notification { get; set; }


        public Sprint(DateTime startDate, DateTime endDate, string name, ISprintState state, SprintReport sprintReport, Pipeline publishingPipeline, List<BacklogItem> backlogItems, List<Member> members)
        {
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
            State = state;
            SprintReport = sprintReport;
            PublishingPipeline = publishingPipeline;
            BacklogItems = backlogItems;
            Members = members;
            this.Notification = new SprintCancelStrategy(new EmailAdapter());
        }

        public void SetNotificationType(ISprintNotificationStrategy type)
        {
            this.Notification = type;
        }

        public void StartSprint()
        {
            this.State = new OngoingState();
        }

        public void CancelSprint()
        {
            this.State.SetToCanceled(this);
        }

        public void EndSprint()
        {
            this.State.SetToFinished(this);
        }
    }
}
