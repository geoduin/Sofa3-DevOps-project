using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.BacklogStates
{
    public class TodoState : IBacklogState
    {
        public void SetDoing(BacklogItem item)
        {
            item.SetBacklogState(new DoingState());
        }

        public void SetToDo(BacklogItem item)
        {
            item.SetBacklogState(new TodoState());
        }

        public void SetToFinished(BacklogItem item)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item)
        {
            throw new NotImplementedException();
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
