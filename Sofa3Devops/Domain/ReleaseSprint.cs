using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class ReleaseSprint : Sprint
    {
        public Pipeline Pipeline { get; set; }
        private IAuthValidationBehavior? authValidation {  get; set; }

        public ReleaseSprint(DateTime startDate, DateTime endDate, string name) : base(startDate, endDate, name)
        {
        }

        public ReleaseSprint(DateTime startDate, DateTime endDate, string name, Pipeline pipeline) : base(startDate, endDate, name)
        {
            Pipeline = pipeline;
        }

        public override void NotifyAll(string title, string message)
        {
            this.NotificationStrategy.SendNotification(title, message, this.Subscribers);
        }

        public override void ChangeSprint(DateTime newStart, DateTime newEnd, string newName)
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

        public bool StartReleasePipeline(Member member) {
            // Validate if pipeline can be started.
            authValidation = new ScrumMasterPOValidation();
            authValidation.HasPermission(member);
            
            // Start pipeline.
            Pipeline.StartPipeline();
            
            if (Pipeline.HasPipelineSucceeded())
            {
                // Notify PO and Scrum-master
                SetNotificationStrategy(new PONotificationStrategy());
                NotifyAll("Pipeline has now started", "Please wait till the pipeline has completed.");
                SetNotificationStrategy(new ScrumMasterNotificationStrategy());
                NotifyAll("Pipeline has now started", "Please wait till the pipeline has completed.");
                return true;
            }
            else
            {
                // Send notification. Notify scrummaster
                SetNotificationStrategy(new ScrumMasterNotificationStrategy());
                NotifyAll("CI/CD Pipeline has failed", "Wait till the scrum-master to decide");
                return false;
            }
            
        }

        public override void EndSprint(Member member)
        {
            StartReleasePipeline(member);
        }
    }
}
