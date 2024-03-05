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
        public void SetToDo(BacklogItem item);
        public void SetDoing(BacklogItem item);
        public void SetToReadyTesting(BacklogItem item);
        public void SetToTesting(BacklogItem item);
        public void SetToTested(BacklogItem item);
        public void SetToFinished(BacklogItem item);
    }
}
