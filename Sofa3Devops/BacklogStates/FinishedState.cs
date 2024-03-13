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
        public void SetDoing(BacklogItem item, Member member)
        {
        }

        public void SetToDo(BacklogItem item, Member member)
        {
        }

        public void SetToFinished(BacklogItem item, Member member)
        {
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
            throw new InvalidOperationException("Can't change finished story to ready for testing");
        }

        public void SetToTested(BacklogItem item, Member member)
        {
        }

        public void SetToTesting(BacklogItem item, Member member)
        {
        }
    }
}
