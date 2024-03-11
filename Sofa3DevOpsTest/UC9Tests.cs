using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC9Tests
    {
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem backlogItem { get; set; }
        private Sprint sprint { get; set; }

        public UC9Tests()
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
        }

        [Fact]
        public void TestInvalidationOfBacklogItem()
        {
            // Arrange
            sprint.AddBacklogItem(backlogItem);
            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(leadDeveloper);

            // Act
            // Domain service should be present.

            Assert.IsType<TodoState>(backlogItem.State);
            // Validate scrummaster notification
        }

        [Fact]
        public void TestReadyForTestingToTodo()
        {
           
        }

        [Fact]
        public void TestReadyForTestingToTodoAndAllOtherActivitiesToTodo()
        {

        }
    }
}
