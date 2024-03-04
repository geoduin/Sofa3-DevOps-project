using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStates
{
    public class CanceledState : ISprintState
    {
        public void SetToCanceled(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void SetToFinished(Sprint sprint)
        {
            throw new NotImplementedException();
        }

        public void SetToOngoing(Sprint sprint)
        {
            throw new NotImplementedException();
        }
    }
}
