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
        public void SetDoing(BacklogItem item, Member member)
        {
            item.SetBacklogState(new DoingState());
        }

        public void SetToDo(BacklogItem item, Member member)
        {
            item.SetBacklogState(new TodoState());
        }

        public void SetToFinished(BacklogItem item, Member m)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
            throw new InvalidOperationException("Item or activity must first be moved to Doing, before it can be moved to ready for testing.");
        }

        public void SetToTested(BacklogItem item, Member m)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item, Member m)
        {
            throw new NotImplementedException();
        }
    }
}
