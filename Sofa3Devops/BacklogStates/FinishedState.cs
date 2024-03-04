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
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToFinished(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Can't change finished story to ready for testing");
        }

        public void SetToTested(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToTesting(BacklogItem item)
        {
            throw new NotImplementedException();
        }
    }
}
