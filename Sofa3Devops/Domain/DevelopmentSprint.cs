using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class DevelopmentSprint : Sprint
    {
        public SprintSummary? Summary { get; set; }

        public DevelopmentSprint(DateTime startDate, DateTime endDate, string name) : base(startDate, endDate, name)
        {
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

        private bool IsSummaryUploaded()
        {
            return Summary != null;
        }

        public override void EndSprint(Member member)
        {
            // Is summary aanwezig
            if (!IsSummaryUploaded())
            {
                throw new InvalidOperationException("Summary is required in order to end the review sprint.");
            }
            
        }
    }
}
