using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStates
{
    public interface ISprintState
    {
        public void SetToOngoing();
        public void SetToFinished();
        public void SetToCanceled();
    }
}
