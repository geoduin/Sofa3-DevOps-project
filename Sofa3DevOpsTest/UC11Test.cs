
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;

namespace Sofa3DevOpsTest
{
    public class UC11Test
    {
        [Fact]
        public void TestersCanSetReadyForTestingItemToTestingIfMemberOfSprint()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now,  DateTime.MaxValue, "test");
            sprint.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new ReadyToTestingState();
            item.SetToTesting(tester);
            Assert.True(item.State.GetType().Equals(typeof(TestingState)));
        }

        [Fact]
        public void TestersCantSetReadyForTestingItemToTestingIfNotMemberOfSprint()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new ReadyToTestingState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToTesting(tester));
        }
        [Fact]
        public void MemberOfSprintCantSetItemToTestingIfRoleIsNotTester()
        {
            Member notTester = new Developer("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(notTester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new ReadyToTestingState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToTesting(notTester));
        }

        [Fact]
        public void TestersCanSetTestingItemToTestedIfMemberOfSprint()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestingState();
            item.SetToTested(tester);
            Assert.True(item.State.GetType().Equals(typeof(TestedState)));
        }

        [Fact]
        public void TestersCantSetTestingItemToTestedIfNotMemberOfSprint()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestingState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToTested(tester));
        }
        [Fact]
        public void MemberOfSprintCantSetItemToTestedIfRoleIsNotTester()
        {
            Member notTester = new Developer("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(notTester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestingState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToTested(notTester));
        }

        //[Fact]
        //public void ProductOwnerCanSetTestedItemToFinishedIfMemberOfSprint()
        //{
        //    Member productOwner = new ProductOwner("test", "test", "test");
        //    Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
        //    sprint.Members.Add(productOwner);
        //    BacklogItem item = new BacklogItem("test", "test");
        //    item.Sprint = sprint;
        //    sprint.AddBacklogItem(item);
        //    item.State = new TestedState();
        //    item.SetToFinished(productOwner);
        //    Assert.True(item.State.GetType().Equals(typeof(FinishedState)));
        //}

        //[Fact]
        //public void ProductOwnerCantSetTestedItemToFinishedIfNotMemberOfSprint()
        //{
        //    Member productOwner = new ProductOwner("test", "test", "test");
        //    Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
        //    BacklogItem item = new BacklogItem("test", "test");
        //    item.Sprint = sprint;
        //    sprint.AddBacklogItem(item);
        //    item.State = new TestedState();
        //    Assert.Throws<UnauthorizedAccessException>(() => item.SetToFinished(productOwner));
        //}

        //[Fact]
        //public void MemberCantSetTestedItemToFinishedIfProductOwner()
        //{
        //    Member notProductOwner = new Developer("test", "test", "test");
        //    Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
        //    sprint.Members.Add(notProductOwner);
        //    BacklogItem item = new BacklogItem("test", "test");
        //    item.Sprint = sprint;
        //    sprint.AddBacklogItem(item);
        //    item.State = new TestedState();
        //    Assert.Throws<UnauthorizedAccessException>(() => item.SetToFinished(notProductOwner));
        //}

        [Fact]
        public void ProductOwnerCanSetTestedItemToToDoIfMemberOfSprint()
        {
            Member productOwner = new ProductOwner("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(productOwner);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestedState();
            item.SetToToDo(productOwner);
            Assert.True(item.State.GetType().Equals(typeof(TodoState)));
        }

        [Fact]
        public void ProductOwnerCantSetTestedItemToToDoIfNotMemberOfSprint()
        {
            Member productOwner = new ProductOwner("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestedState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToToDo(productOwner));
        }

        [Fact]
        public void MemberCantSetTestedItemToTodoIfProductOwner()
        {
            Member notProductOwner = new Developer("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(notProductOwner);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestedState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToToDo(notProductOwner));
        }

        [Fact]
        public void SettingItemFromTestingToToDoTriggersSendingOfNotification()
        {
            Member tester = new Tester("test", "test", "test");
            var mockSprint = new Mock<Sprint>(DateTime.Now, DateTime.MaxValue, "test");
            mockSprint.Object.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = mockSprint.Object;
            item.State = new TestingState();
            item.SetToToDo(tester);
            mockSprint.Verify(m => m.NotifyAll(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void SettingItemFromTestingToToDoTriggersSendingOfNotificationTo()
        {
            Member tester = new Tester("test", "test", "test");
            var mockSprint = new Mock<Sprint>(DateTime.Now, DateTime.MaxValue, "test");
            mockSprint.Object.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = mockSprint.Object;
            item.State = new TestingState();
            item.SetToToDo(tester);
            mockSprint.Verify(m => m.NotifyAll(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
