using DomainServices.DomainServicesImpl;
using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class SprintCreationTest
    {
        [Fact]
        public void TestDevelopmentSprintCreation()
        {
            SprintManager sprintManager = new SprintManager(new DevelopmentSprintFactory());
            ScrumMaster scrumMaster = new("", "", "");
            var developmentSprint = sprintManager.CreateSprint(DateTime.Now, DateTime.Now.AddDays(2), "Release sprint", scrumMaster);

            Assert.IsType<DevelopmentSprint>(developmentSprint);
        }


        [Fact]
        public void TestReleaseSprintCreation()
        {
            SprintManager sprintManager = new SprintManager(new ReleaseSprintFactory());
            ScrumMaster scrumMaster = new("", "", "");
            var releaseSprint = sprintManager.CreateSprint(DateTime.Now, DateTime.Now.AddDays(2), "Release sprint", scrumMaster);
        
            Assert.IsType<ReleaseSprint>(releaseSprint);
        }
    }
}
