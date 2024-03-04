using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.SprintStates
{
    public class CanceledState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            throw new InvalidOperationException();
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
