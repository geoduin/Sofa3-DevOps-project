using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.BacklogStates
{
    public interface IBacklogState
    {
        public void SetToDo();
        public void SetDoing();
        public void SetToReadyTesting();
        public void SetToTesting();
        public void SetToTested();
        public void SetToFinished();
    }
}
