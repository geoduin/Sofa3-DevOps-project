using DomainServices.DomainServicesImpl;
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC7Tests
    {
        private Sprint testSprint {  get; set; }
        private Member dev {  get; set; }

        public UC7Tests() {
            testSprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "");
            dev = new Developer("", "", "");
        }

        [Fact]
        public void TestDoingToReadyForTesting()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "");
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            

            BacklogItem backlogItem = new BacklogItem("S", "s")
            {
                ResponsibleMember = new Developer("Herman", "Herr@example.com", "HerrSlack"),
                State = new DoingState()
            };
            sprint.AddBacklogItem(backlogItem);
            mockedNotificationStrategy.Setup(x => x.SendNotification("", "", backlogItem.Subscribers));
            backlogItem.Sprint.SetNotificationStrategy(mockedNotificationStrategy.Object);
            
            backlogItem.SetItemReadyForTesting(new Developer("Herman", "Herr@example.com", "HerrSlack"));

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

            backlogItem.State.SetToDo(backlogItem, dev);
            Assert.IsType<TodoState>(backlogItem.State);
        }

        [Fact]
        public void TestItemFromDoingToDoingToValidateNothingHappens()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "")
            {
                State = new DoingState()
            };

            backlogItem.SetToDoing(new Developer("", "", ""));

            Assert.IsType<DoingState>(backlogItem.State);
        }

        [Fact]
        public void TestDoingToReadyTestingViaService()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "");
            BacklogStateManager backlogStateManager = new BacklogStateManager();
            
            BacklogItem backlogItem = new BacklogItem("Task1", "")
            {
                State = new DoingState()
            };
            sprint.AddBacklogItem(backlogItem);
            sprint.SetNotificationStrategy(new Mock<INotificationStrategy>().Object);
            Member member = new Developer("Dave", "Dave@example.com", "");
            backlogStateManager.SetItemForReadyTesting(member, backlogItem);

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }
    }
}
