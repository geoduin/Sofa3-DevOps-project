using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sofa3Devops.SprintStates
{
    public class FinishedState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            sprint.State = this;
            if (sprint.GetType().Equals(typeof(ReleaseSprint)))
            {
                sprint.NotifyAll($"Update over {sprint.Name}", $"Backlog item {sprint.Name} has been updated to {sprint.State}");

            }
        }

        public void SetToFinished(Sprint sprint)
        {
            throw new InvalidOperationException();
        }

        public void SetToOngoing(Sprint sprint)
        {
            throw new InvalidOperationException();
        }
    }
}
