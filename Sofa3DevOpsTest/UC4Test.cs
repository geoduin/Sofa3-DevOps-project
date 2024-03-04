﻿using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStrategies;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC4Test
    {

        [Fact]
        public void TestSprintCancelation()
        {
            ScrumMaster scrumMaster = new ScrumMaster("Scrum");
            scrumMaster.SetSprintStrategy(new AuthorizedSprintStrategy(new ReleaseSprintFactory()));
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint canceled");

            scrumMaster.CancelSprint(sprint);

            Assert.IsType<CanceledState>(sprint.State);
        }

        [Fact]
        public void TestSprintCancelingNonAuthorised() {
            Developer dev = new Developer("Scrum");
            dev.SetSprintStrategy(new NonAuthorizedSprintStrategy());
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint canceled");

            var error = Assert.Throws<UnauthorizedAccessException>(()=> dev.CancelSprint(sprint));

            Assert.Equal("Does not have the right authorization to perform this action.", error.Message);
        }
    }
}
