using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.BacklogStates
{
    public class FinishedState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
        }

        public void SetToDo(BacklogItem item)
        {
        }

        public void SetToFinished(BacklogItem item)
        {
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Can't change finished story to ready for testing");
        }

        public void SetToTested(BacklogItem item)
        {
        }

        public void SetToTesting(BacklogItem item)
        {
        }
    }
}
