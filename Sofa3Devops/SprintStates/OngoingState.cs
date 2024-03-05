using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sofa3Devops.Domain;

namespace Sofa3Devops.SprintStates
{
    public class OngoingState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            sprint.State = new CanceledState();
            if (sprint.GetType().Equals(typeof(ReleaseSprint)))
            {
                sprint.NotifyAll($"Update about Sprint {sprint.Name}", $"The release for sprint {sprint.Name} has been canncelled");
            }
        }

        public void SetToFinished(Sprint sprint)
        {
            sprint.State = new FinishedState();
        }

        public void SetToOngoing(Sprint sprint)
        {
            throw new InvalidOperationException();
        }
    }
}
