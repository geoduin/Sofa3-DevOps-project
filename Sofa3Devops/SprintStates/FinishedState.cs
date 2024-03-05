using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStates
{
    public class FinishedState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            sprint.State = this;
            sprint.NotifyAll();
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
