using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;

namespace Sofa3Devops.BacklogStates
{
    public interface IBacklogState
    {
        public void SetToDo(BacklogItem item, Member member);
        public void SetDoing(BacklogItem item, Member member);
        public void SetToReadyTesting(BacklogItem item, Member member);
        public void SetToTesting(BacklogItem item, Member member);
        public void SetToTested(BacklogItem item, Member member);
        public void SetToFinished(BacklogItem item, Member member);
    }
}
