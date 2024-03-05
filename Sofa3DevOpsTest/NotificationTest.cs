using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.SprintStates;

namespace Sofa3DevOpsTest
{
    public class NotificationTest
    {
        [Fact]
        public void TestNotificationShouldOnlySendNotificationToTesters()
        {
            var handler = new Mock<INotificationAdapter>();
            INotificationStrategy notification = new TesterNotificationStrategy();
            BacklogItem item = new BacklogItem("Test", "Test");
            List<BacklogItem> items = new List<BacklogItem>();
            items.Add(item);
            Member testMember = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member notTestMember = new ProductOwner("ingrid", "ingrid@ingrid.nl", "ingrid");
            List<Member> members = new List<Member>();
            members.Add(testMember);
            members.Add(notTestMember);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members;
            item.Sprint = sprint;
            //notification.SendNotification(item);
            List<Member> expectedList = new List<Member>();
            expectedList.Add(testMember);

            //handler.Verify(x => x.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), expectedList), Times.Exactly(1));
        }

        [Fact]
        public void TestNotificationShouldSendNotificationToAllTesters()
        {
            var handler = new Mock<INotificationAdapter>();
            INotificationStrategy notification = new TesterNotificationStrategy();
            BacklogItem item = new BacklogItem("Test", "Test");
            List<BacklogItem> items = new List<BacklogItem>();
            items.Add(item);
            Member testMember = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member testMember2 = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member notTestMember = new ProductOwner("ingrid", "ingrid@ingrid.nl", "ingrid");
            List<Member> members = new List<Member>();
            members.Add(testMember);
            members.Add(notTestMember);
            members.Add(testMember2);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members; item.Sprint = sprint;
            //notification.SendNotification(item);
            List<Member> expectedList = new List<Member>();
            expectedList.Add(testMember);
            expectedList.Add(testMember2);
            //handler.Verify(x => x.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), expectedList), Times.Exactly(1));
        }

        [Fact]
        public void AllNotificationShouldSendNotificationToAllMembers()
        {
            var handler = new Mock<INotificationAdapter>();
            INotificationStrategy notification = new AllNotificationStrategy();
            BacklogItem item = new BacklogItem("Test", "Test");
            List<BacklogItem> items = new List<BacklogItem>();
            items.Add(item);
            Member testMember = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member testMember2 = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member notTestMember = new ProductOwner("ingrid", "ingrid@ingrid.nl", "ingrid");
            List<Member> members = new List<Member>();
            members.Add(testMember);
            members.Add(notTestMember);
            members.Add(testMember2);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members; item.Sprint = sprint;
            //notification.SendNotification(item);
            List<Member> expectedList = new List<Member>();
            expectedList.Add(testMember);
            expectedList.Add(notTestMember);
            expectedList.Add(testMember2);
            //handler.Verify(x => x.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), expectedList), Times.Exactly(1));
        }
        //EmailClient is not mockable, so the INotification itself cannot be tested directly
        //[Fact]
        //public void EmailAdapterShouldSendEmail()
        //{
        //    var mockClient = new Mock<EmailClient>();
        //    Member testMember = new Tester("Henk", "henk@henk.nl", "realHenk");
        //    //Member testMember2 = new Tester("Henk", "henk@henk.nl", "realHenk");
        //    //Member notTestMember = new ProductOwner("ingrid", "ingrid@ingrid.nl", "ingrid");
        //    List<Member> expectedList = new List<Member>();
        //    expectedList.Add(testMember);
        //    //expectedList.Add(testMember2);
        //    //expectedList.Add(notTestMember);

        //    INotification sut = new EmailAdapter(mockClient.Object);
        //    sut.SendNotification("test", "test", DateTime.Now, expectedList);
        //    mockClient.Verify(c => c.SendToMail("test", "teest", "test", "test"), Times.AtLeastOnce);
        //}

        [Fact]
        public void SettingSprintToCancelledShouldTriggerNotificationIfInitialStateIsOngoing()
        {

            Member tester = new Tester("henk", "henk@henk.nl", "realHenk");
            Member productOwner = new ProductOwner("ingrid", "ingrid@ingrid.nl", "realIngrid");
            List<Member> members = new List<Member>();
            members.Add(tester);
            members.Add(productOwner);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members; BacklogItem backlogItem = new BacklogItem("test", "test");
            backlogItem.State = new DoingState();
            //var sprintNotification = new Mock<ISprintNotificationStrategy>();
            //sprint.Notification = sprintNotification.Object;
            //backlogItem.State.SetToReadyTesting(backlogItem);

            sprint.State = new OngoingState();
            sprint.State.SetToCanceled(sprint);
            //sprintNotification.Verify(c => c.SendNotification(sprint), Times.AtLeastOnce);
        }

        [Fact]
        public void SettingSprintToCancelledShouldTriggerNotificationIfInitialStateIsFinished()
        {

            Member tester = new Tester("henk", "henk@henk.nl", "realHenk");
            Member productOwner = new ProductOwner("ingrid", "ingrid@ingrid.nl", "realIngrid");
            List<Member> members = new List<Member>();
            members.Add(tester);
            members.Add(productOwner);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members; BacklogItem backlogItem = new BacklogItem("test", "test");
            backlogItem.State = new DoingState();
            //var sprintNotification = new Mock<ISprintNotificationStrategy>();
            //sprint.Notification = sprintNotification.Object;
            //backlogItem.State.SetToReadyTesting(backlogItem);

            sprint.State = new Sofa3Devops.SprintStates.FinishedState();
            sprint.State.SetToCanceled(sprint);
            //sprintNotification.Verify(c => c.SendNotification(sprint), Times.AtLeastOnce);
        }

        [Fact]
        public void SettingSprintToCancelledShouldSendNotificationToAllMembers()
        {

            Member testMember = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member testMember2 = new Tester("Henk", "henk@henk.nl", "realHenk");
            Member notTestMember = new ProductOwner("ingrid", "ingrid@ingrid.nl", "ingrid");
            List<Member> members = new List<Member>();
            members.Add(testMember);
            members.Add(notTestMember);
            members.Add(testMember2);
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "a sprint");
            sprint.Members = members;
            var mockHandler = new Mock<INotificationAdapter>();
            //ISprintNotificationStrategy sut = new SprintCancelStrategy(mockHandler.Object);
            //sut.SendNotification(sprint);
            //mockHandler.Verify(c => c.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), members), Times.Exactly(1));
        }

    }
}
