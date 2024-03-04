﻿using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintStates
{
    public interface ISprintState
    {
        public void SetToOngoing(Sprint sprint);
        public void SetToFinished(Sprint sprint);
        public void SetToCanceled(Sprint sprint);
    }
}
