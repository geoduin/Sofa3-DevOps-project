using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC7Tests
    {
        [Fact]
        public void TestDoingToReadyForTesting()
        {

            BacklogItem backlogItem = new BacklogItem("S", "s")
            {
                ResponsibleMember = new Developer("Herman", "Herr@example.com", "HerrSlack"),
                State = new DoingState()
            };
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            mockedNotificationStrategy.Setup(x => x.SendNotification(backlogItem)).Verifiable();
            backlogItem.NotificationStrategy = mockedNotificationStrategy.Object;

            backlogItem.SetItemReadyForTesting();

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }
        // Future test to validate if all testers receive the message.
        [Fact(Skip = "Notification functionality is still in development, should be tested at a later stage.")]
        public void TestReadyForTestingReceiveeTestersNotification()
        {
            BacklogItem backlogItem = new BacklogItem("S", "s")
            {
                ResponsibleMember = new Developer("Herman", "Herr@example.com", "HerrSlack"),
                State = new DoingState()
            };
            backlogItem.SetItemReadyForTesting();

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }

        // Other test, to ensure the state follows the state diagram
        [Fact]
        public void TestDoingToTodo()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "")
            {
                State = new DoingState()
            };

            backlogItem.State.SetToDo(backlogItem);
            Assert.IsType<TodoState>(backlogItem.State);
        }

        [Fact]
        public void TestItemFromDoingToDoingToValidateNothingHappens()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "")
            {
                State = new DoingState()
            };

            backlogItem.SetToDoing();

            Assert.IsType<DoingState>(backlogItem.State);
        }
    }
}
