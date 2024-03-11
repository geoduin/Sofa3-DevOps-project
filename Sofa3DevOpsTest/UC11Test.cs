using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintStates;

namespace Sofa3DevOpsTest
{
    public class UC11Test
    {
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem backlogItem { get; set; }
        private Sprint sprint { get; set; }
        private readonly IBacklogStateManager stateManager;

        public UC11Test()
        {
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster = new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            leadDeveloper.SetLeadDeveloper();
            backlogItem = new BacklogItem("Head task", "")
            {
                State = new TestingState(),
                ResponsibleMember = developer
            };
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project")
            {
                State = new OngoingState()
            };
            stateManager = new BacklogStateManager();
        }

        [Fact]    
        public void TestTestingToTestedByTester()
        {
            sprint.AddBacklogItem(backlogItem);
            // Act
            stateManager.FinishTesting(tester, backlogItem);
            // Assert
            Assert.IsType<TestedState>(backlogItem.State);
        }

        [Fact]
        public void TestTestingToTestedByTesterAndAllActivities()
        {
            Activity act1 = new Activity("", "", backlogItem)
            {
                State = new ReadyToTestingState()
            };
            Activity act2 = new Activity("", "", backlogItem)
            {
                State = new ReadyToTestingState()
            };
            sprint.AddBacklogItem(backlogItem);
            backlogItem.AddActivityToBacklogItem(act1);
            backlogItem.AddActivityToBacklogItem(act2);

            // Act
            stateManager.FinishTesting(tester, backlogItem);

            // Assert
            Assert.IsType<TestedState>(act1.State);
            Assert.IsType<TestedState>(act2.State);
        }

        [Fact]
        public void TestTestingToTestedByNonTester()
        {
            var error = Assert.Throws<UnauthorizedAccessException>(() => stateManager.FinishTesting(developer, backlogItem));
            var errorScrumMaster = Assert.Throws<UnauthorizedAccessException>(() => stateManager.FinishTesting(scrumMaster, backlogItem));
            var errorLeadDeveloper = Assert.Throws<UnauthorizedAccessException>(() => stateManager.FinishTesting(leadDeveloper, backlogItem));

            // Validate scrummaster notification
            Assert.Equal("Unauthorized action: Users with Developer role are not allowed to set item to Tested. Only testers are allowed to move backlog-item to Tested.", error.Message);
            Assert.Equal("Unauthorized action: Users with Scrum-master role are not allowed to set item to Tested. Only testers are allowed to move backlog-item to Tested.", errorScrumMaster.Message);
            Assert.Equal("Unauthorized action: Users with Lead developer role are not allowed to set item to Tested. Only testers are allowed to move backlog-item to Tested.", errorLeadDeveloper.Message);
        }

    }
}
