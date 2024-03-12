using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
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
            var error = Assert.Throws<UnauthorizedAccessException>(()=> backlogItem.SetToTodo(developer));
            var errorScrumMaster = Assert.Throws<UnauthorizedAccessException>(() => backlogItem.SetToTodo(scrumMaster));
            var errorLeadDeveloper = Assert.Throws<UnauthorizedAccessException>(() => backlogItem.SetToTodo(leadDeveloper));

            // Validate scrummaster notification
            Assert.Equal("Unauthorized action: Users with Developer role are not allowed to perform this action. Only testers are allowed.", error.Message);
            Assert.Equal("Unauthorized action: Users with Scrum-master role are not allowed to perform this action. Only testers are allowed.", errorScrumMaster.Message);
            Assert.Equal("Unauthorized action: Users with Lead developer role are not allowed to perform this action. Only testers are allowed.", errorLeadDeveloper.Message);
        }

        [Fact]
        public void TestReadyForTestingToTodo()
        {
            backlogItem.State = new ReadyToTestingState();
            sprint.AddBacklogItem(backlogItem);
            backlogItem.SetToTodo(tester);

            Assert.IsType<TodoState>(backlogItem.State);
        }

        [Fact]
        public void TestReadyForTestingToTodoAndAllOtherActivitiesToTodo()
        {
            Activity act1 = new Activity("", "", backlogItem)
            {
                State = new ReadyToTestingState()
            };
            Activity act2 = new Activity("", "", backlogItem)
            {
                State = new ReadyToTestingState()
            };
            backlogItem.State = new ReadyToTestingState();
            sprint.AddBacklogItem(backlogItem);
            backlogItem.AddActivityToBacklogItem(act1);
            backlogItem.AddActivityToBacklogItem(act2);
            
            // Act
            backlogItem.SetToTodo(tester);

            Assert.IsType<TodoState>(act1.State);
            Assert.IsType<TodoState>(act2.State);
        }
    }
}
