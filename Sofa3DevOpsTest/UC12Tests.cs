using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC12Tests
    {
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem readyForTestingItem { get; set; }
        private BacklogItem testingItem { get; set; }
        private Sprint sprint { get; set; }
        private IBacklogStateManager backlogStateManager { get; set; }

        private Mock<Subscriber> leadDevSubscriber { get; set; }
        private Mock<Subscriber> devSubscriber { get; set; }
        private Mock<Subscriber> testerSubscriber { get; set; }
        private Mock<Subscriber> scrumMasterSubscriber { get; set; }

        public UC12Tests()
        {
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster =  new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            leadDeveloper.SetLeadDeveloper();

            readyForTestingItem = new BacklogItem("Head task", "")
            {
                State = new ReadyToTestingState(),
                ResponsibleMember = developer
            };
            testingItem = new BacklogItem("Head task", "")
            {
                State = new TestingState(),
                ResponsibleMember = developer
            };
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project")
            {
                State = new OngoingState()
            };
            backlogStateManager = new BacklogStateManager();

            sprint.AddBacklogItem(testingItem);
            sprint.AddBacklogItem(readyForTestingItem);

            leadDevSubscriber = new Mock<Subscriber>(leadDeveloper);
            devSubscriber = new Mock<Subscriber>(developer);
            testerSubscriber = new Mock<Subscriber>(tester);
            scrumMasterSubscriber = new Mock<Subscriber>(scrumMaster);

            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(leadDeveloper);
            sprint.AssignMembersToSprint(scrumMaster);

            sprint.AddSubscriber(testerSubscriber.Object);
            sprint.AddSubscriber(leadDevSubscriber.Object);
            sprint.AddSubscriber(devSubscriber.Object); 
            sprint.AddSubscriber(scrumMasterSubscriber.Object);

        }

        [Fact]
        public void TestIfSubscribersArePresent()
        {
            var dick = sprint.Subscribers;

            //Act
            var result = dick.GetValueOrDefault(typeof(Tester));
            var resultDevelopers = dick.GetValueOrDefault(typeof(Developer));
            var resultScrumMaster = dick.GetValueOrDefault(typeof(ScrumMaster));

            Assert.Single(result!);
            Assert.Equal(2, resultDevelopers!.Count);
            Assert.Single(resultScrumMaster!);
        }

        [Fact]
        public void TestIfScrumMasterIsNotifiedWhenItemIsRejectedByTesterInReadyTestingState()
        {
            // Ready testing -> Todo
            Tester tester = new Tester("", "", "");
            // Act
            backlogStateManager.SetItemBackToTodo(tester, readyForTestingItem);
            
            // Assert
            leadDevSubscriber.Verify(x => x.Notify($"Backlog-item: {readyForTestingItem.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {readyForTestingItem.State.GetType().Name}"), Times.Never());
            devSubscriber.Verify(x => x.Notify($"Backlog-item: {readyForTestingItem.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {readyForTestingItem.State.GetType().Name}"), Times.Never());
            testerSubscriber.Verify(x => x.Notify($"Backlog-item: {readyForTestingItem.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {readyForTestingItem.State.GetType().Name}"), Times.Never());
            scrumMasterSubscriber.Verify(x => x.Notify($"Backlog-item: {readyForTestingItem.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {readyForTestingItem.State.GetType().Name}"), Times.Once());
        }
    }
}
