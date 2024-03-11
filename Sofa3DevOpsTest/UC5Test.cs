using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC5Test
    {
        private readonly AbstractSprintFactory devSprintFactory;
        private readonly AbstractSprintFactory releaseSprintFactory;

        private readonly ISprintManager sprintManager;
        private readonly ISprintManager sprintManagerRelease;

        public UC5Test()
        {
            devSprintFactory = new DevelopmentSprintFactory();
            releaseSprintFactory = new ReleaseSprintFactory();
            sprintManager = new SprintManager(devSprintFactory);
            sprintManagerRelease = new SprintManager(releaseSprintFactory);
        }

        [Fact]
        public void TestAddBacklogItemByScrumMaster()
        {
            ScrumMaster master = new ScrumMaster("George", "dev@dev.nl", "dev@dev.nl");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now, "Sprint 1");
            BacklogItem backlogItem = new BacklogItem("Task 1", "NA");
 
            var result = sprintManager.AddBacklogItem(sprint, backlogItem, master);

            Assert.NotNull(result);
            Assert.Single(sprint.BacklogItems);
        }

        [Fact]
        public void TestAddBacklogItemByDeveloper()
        {
            Developer master = new Developer("George", "dev@dev.nl", "dev@dev.nl");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now, "Sprint 1");
            BacklogItem backlogItem = new BacklogItem("Task 1", "NA");
            
            var result = Assert.Throws<UnauthorizedAccessException>(()=> sprintManager.AddBacklogItem(sprint, backlogItem, master));

            Assert.Empty(sprint.BacklogItems);
            Assert.Equal("Does not have the right authorization to perform this action.", result.Message);
        }

        [Fact]
        public void TestChangeSprintBeforeStarting()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 1");
            sprint.ChangeSprint(new DateTime(2023, 1, 20), new DateTime(2023, 2, 27), "Changed sprint");

            Assert.Equal("Changed sprint", sprint.Name);
            Assert.Equal(new DateTime(2023, 1, 20), sprint.StartDate);
            Assert.Equal(new DateTime(2023, 2, 27), sprint.EndDate);
        }

        [Fact]
        public void TestChangeSprintAfterStarting()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 1")
            {
                State = new OngoingState()
            };
            var error = Assert.Throws<InvalidOperationException>(() => sprint.ChangeSprint(new DateTime(2023, 1, 20), new DateTime(2023, 2, 27), "Changed sprint"));

            Assert.Equal("Backlog items cannot be changed on ongoing sprint", error.Message);
        }
    }
}
