using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC10
    {
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem backlogItem { get; set; }
        private Sprint sprint { get; set; }

        public UC10() {
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster = new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            leadDeveloper.SetLeadDeveloper();
            backlogItem = new BacklogItem("Head task", "")
            {
                State = new ReadyToTestingState(),
                ResponsibleMember = developer
            };
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project")
            {
                State = new OngoingState()
            };
        }

        [Fact]
        public void TestItemFromReadyForTestingToTesting()
        {
            backlogItem.SetToTesting(tester);
            Assert.IsType<TestingState>(backlogItem.State);
        }
        
        [Fact]
        public void TestItemFromReadyForTestingToTestingByNonTester()
        {
            var error = Assert.Throws<UnauthorizedAccessException>(() => backlogItem.SetToTesting(developer));
            var errorScrumMaster = Assert.Throws<UnauthorizedAccessException>(() => backlogItem.SetToTesting(scrumMaster));
            var errorLeadDeveloper = Assert.Throws<UnauthorizedAccessException>(() => backlogItem.SetToTesting(leadDeveloper));

            // Validate scrummaster notification
            Assert.Equal("Unauthorized action: Users with Developer role are not allowed to perform this action. Only testers are allowed.", error.Message);
            Assert.Equal("Unauthorized action: Users with Scrum-master role are not allowed to perform this action. Only testers are allowed.", errorScrumMaster.Message);
            Assert.Equal("Unauthorized action: Users with Lead developer role are not allowed to perform this action. Only testers are allowed.", errorLeadDeveloper.Message);
        }
    }
}
