using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC7Tests
    {
        private Sprint testSprint {  get; set; }

        public UC7Tests() {
            testSprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "");
        }

        [Fact]
        public void TestDoingToReadyForTesting()
        {
            
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            

            BacklogItem backlogItem = new BacklogItem("S", "s")
            {
                ResponsibleMember = new Developer("Herman", "Herr@example.com", "HerrSlack"),
                State = new DoingState()
            };

            mockedNotificationStrategy.Setup(x => x.SendNotification("", "", backlogItem.Subscribers));
            testSprint.AddBacklogItem(backlogItem);
            testSprint.SetNotificationStrategy(mockedNotificationStrategy.Object);
            
            backlogItem.SetItemReadyForTesting();

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }

        // Future test to validate if all testers receive the message.
        [Fact]
        public void TestReadyForTestingReceiveeTestersNotification()
        {
            var test1Subscriber = new Mock<RegularSubscriber>(new Tester("test", "test", "test"));
            var test2Subscriber = new Mock<RegularSubscriber>(new Tester("test", "test", "test"));


            BacklogItem backlogItem = new BacklogItem("S", "s")
            {
                ResponsibleMember = new Developer("Herman", "Herr@example.com", "HerrSlack"),
                State = new DoingState()
            };
            backlogItem.AddSubscriber(test1Subscriber.Object);
            backlogItem.AddSubscriber(test2Subscriber.Object);
            testSprint.AddBacklogItem(backlogItem);
            testSprint.SetNotificationStrategy(new TesterNotificationStrategy());

            // Act
            backlogItem.SetItemReadyForTesting();

            // Assert
            Assert.Equal("Sofa3Devops.Domain.Tester", test1Subscriber.Object.NotifiedUser.ToString());
            Assert.IsType<ReadyToTestingState>(backlogItem.State);
            //test1Subscriber.Verify(x => x.Notify("", ""), Times.Exactly(1));
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
