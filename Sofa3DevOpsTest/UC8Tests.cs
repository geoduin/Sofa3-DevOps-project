using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC8Tests
    {
        private Member tester {  get; set; }
        private Member developer { get; set; }
        private Member scrumMaster  { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem backlogItem { get; set; }
        private Sprint sprint { get; set; }

        public UC8Tests() {
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
        public void TestValidationOfBacklogItemByTester()
        {
            

            sprint.AddBacklogItem(backlogItem);
            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(leadDeveloper);

            tester.ApproveItemForTesting(backlogItem);

            Assert.IsType<TestingState>(backlogItem.State);
        }

        [Fact]
        public void TestInvalidationOfBacklogItem()
        {
            sprint.AddBacklogItem(backlogItem);
            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(leadDeveloper);

            tester.DisapproveItemForTesting(backlogItem);

            Assert.IsType<TodoState>(backlogItem.State);
            // Validate scrummaster notification
        }

        [Fact]
        public void TestValidationOfBacklogItemByOtherPeople()
        {
            var error = Assert.Throws<UnauthorizedAccessException>(() => developer.ApproveItemForTesting(backlogItem));

            Assert.Equal("Does not have authority to approve item for testing. Only testers are allowed to move.", error.Message);
        }

        [Fact]
        public void TestDisapprovalOfBacklogItemForTestingByOtherPeople()
        {
            var error = Assert.Throws<UnauthorizedAccessException>(() => developer.DisapproveItemForTesting(backlogItem));

            Assert.Equal("Does not have authority to disapprove item for testing. Only testers are allowed to move.", error.Message);
        }

    }
}
