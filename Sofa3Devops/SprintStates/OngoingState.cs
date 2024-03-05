using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.SprintStates
{
    public class OngoingState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            sprint.State = new CanceledState();
            sprint.NotifyAll();
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
