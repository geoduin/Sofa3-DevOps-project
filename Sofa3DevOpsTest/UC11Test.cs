
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using TodoState = Sofa3Devops.BacklogStates.TodoState;

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
        
        
        [Fact]
        public void MemberCantSetTestedItemToToDoIfNotMemberOfSprint()
        {
            Member productOwner = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestedState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToToDo(productOwner));
        }

        [Fact]
        public void MemberCantSetTestedItemToTodoIfNotTester()
        {
            Member productOwner = new ProductOwner("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(productOwner);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestedState();
            Assert.Throws<UnauthorizedAccessException>(() => item.SetToToDo(productOwner));
        }

        [Fact]
        public void AuthorizedTesterCanSetTestedItemToTodoIfStateIsTesting()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new TestingState();
            item.SetToToDo(tester);
            Assert.True(item.State.GetType() == typeof(TodoState));
        }

        [Fact]
        public void AuthorizedTesterCanSetItemToToDoIfStateIsReadyForTesting()
        {
            Member tester = new Tester("test", "test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.Members.Add(tester);
            BacklogItem item = new BacklogItem("test", "test");
            item.Sprint = sprint;
            sprint.AddBacklogItem(item);
            item.State = new ReadyToTestingState();
            item.SetToToDo(tester);
            Assert.True(item.State.GetType() == typeof(TodoState));
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
        public void ScrumMasterStrategyOnlySendsNotificationToScrumMasters()
        {
            Member sm1 = new ScrumMaster("test", "test", "test");
            var s1 = new Mock<Subscriber>(sm1);
            Member sm2 = new ScrumMaster("test", "test", "test");
            var s2 = new Mock<Subscriber>(sm2);
            Member t1 = new Tester("test", "test", "test");
            var s3 = new Mock<Subscriber>(sm1);
            INotificationStrategy strategy = new ScrumMasterStrategy();
            Dictionary<Type, List<Subscriber>> subDictionary = new Dictionary<Type, List<Subscriber>>();
            subDictionary[typeof(ScrumMaster)] = new List<Subscriber>();
            subDictionary[typeof(ScrumMaster)].Add(s1.Object);
            subDictionary[typeof(ScrumMaster)].Add(s2.Object);
            subDictionary[typeof(Tester)] = new List<Subscriber>();
            subDictionary[typeof(Tester)].Add(s3.Object);

            strategy.SendNotification("test", "test", subDictionary);

            s1.Verify(m => m.Notify(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
            s2.Verify(m => m.Notify(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
            s3.Verify(m => m.Notify(It.IsAny<string>(), It.IsAny<string>()), Times.Never());




        }
    }
}
