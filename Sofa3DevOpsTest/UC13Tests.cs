using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
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
    public class UC13Tests
    {
        private Sprint sprint { get; set; }
        private readonly IBacklogStateManager backlogStateManager;

        public UC13Tests() {
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project");
            backlogStateManager = new BacklogStateManager();
        }

        // Backlog item logic
        [Fact]
        public void TestSingleBacklogTest()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };

            var result = backlogItem.HasAllTaskBeenCompleted();

            Assert.True(result);
        }

        [Fact]
        public void TestSingleBacklogTestIncompleteTesting()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestingState()
            };

            var result = backlogItem.HasAllTaskBeenCompleted();

            Assert.False(result);
        }

        [Fact]
        public void TestBacklogItemWithActivitiesUncompleted()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity = new Activity("", "", backlogItem) { State = new TodoState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestedState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            var result = backlogItem.HasAllTaskBeenCompleted();

            Assert.False(result);
        }

        [Fact]
        public void TestBacklogItemWithActivitiesCompleted()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestedState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            var result = backlogItem.HasAllTaskBeenCompleted();

            Assert.True(result);
        }

        [Fact]
        public void TestActivityIfFinishedStateStillReturnsTrue()
        {

            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity1 = new Activity("", "", backlogItem) { State = new FinishedState() };

            backlogItem.AddActivityToBacklogItem(activity1);
            sprint.AddBacklogItem(backlogItem);

            var result = activity1.HasAllTaskBeenCompleted();

            Assert.True(result);
        }

        // State changes
        [Fact]
        public void TestUnAuthorizedItemDisapprove()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };

            Developer randomDeveloper = new Developer("Dic", "", "");
            Tester quackDeveloper = new Tester("Rick", "", "");
            ScrumMaster daveDeveloper = new ScrumMaster("Dave", "", "");

            var error1 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.RejectTestedItem(randomDeveloper, backlogItem));
            var error2 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.RejectTestedItem(quackDeveloper, backlogItem));
            var error3 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.RejectTestedItem(daveDeveloper, backlogItem));

            var firstError = $"Unauthorized action: Users with {randomDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";
            var secondError = $"Unauthorized action: Users with {quackDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";
            var thirdError = $"Unauthorized action: Users with {daveDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";

            Assert.Equal(firstError, error1.Message);
            Assert.Equal(secondError, error2.Message);
            Assert.Equal(thirdError, error3.Message);
        }

        [Fact]
        public void TestUnAuthorizedItemFinished()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };

            Developer randomDeveloper = new Developer("Dic", "", "");
            Tester quackDeveloper = new Tester("Rick", "", "");
            ScrumMaster daveDeveloper = new ScrumMaster("Dave", "", "");

            var error1 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.FinishItem(randomDeveloper, backlogItem));
            var error2 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.FinishItem(quackDeveloper, backlogItem));
            var error3 = Assert.Throws<UnauthorizedAccessException>(() => backlogStateManager.FinishItem(daveDeveloper, backlogItem));

            var firstError = $"Unauthorized action: Users with {randomDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";
            var secondError = $"Unauthorized action: Users with {quackDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";
            var thirdError = $"Unauthorized action: Users with {daveDeveloper} role are not allowed to perform this action. Only lead developers are allowed.";

            Assert.Equal(firstError, error1.Message);
            Assert.Equal(secondError, error2.Message);
            Assert.Equal(thirdError, error3.Message);
        }

        [Fact]
        public void TestBacklogItemTestedToTestedValid()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "");
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            sprint.AddBacklogItem(backlogItem);
            backlogItem.Sprint!.SetNotificationStrategy(new AllNotificationStrategy());
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestedState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            backlogStateManager.FinishItem(randomDeveloper, backlogItem);

            Assert.IsType<FinishedState>(backlogItem.State);
        }

        [Fact]
        public void TestBacklogItemInvalidTestedToTested()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestingState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            var error = Assert.Throws<InvalidOperationException>(()=> backlogStateManager.FinishItem(randomDeveloper, backlogItem));

            Assert.IsNotType<FinishedState>(backlogItem.State);
            Assert.Equal("All tasks and subtasks need to be completed, before finishing this backlog item", error.Message);
        }

        [Fact]
        public void TestAuthorizedItemDisapprove()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "");
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestingState() };
            sprint.AddBacklogItem(backlogItem);
            sprint.AssignMembersToSprint(randomDeveloper);

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            backlogItem.Sprint.SetNotificationStrategy(new TesterNotificationStrategy());
            
            randomDeveloper.SetLeadDeveloper();
            Tester tester = new Tester("Bob", "Test@example.com", "");
            backlogItem.AddSubscriber(new RegularSubscriber(tester));
            sprint.AddBacklogItem(backlogItem);

            backlogStateManager.RejectTestedItem(randomDeveloper, backlogItem);

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }

        [Fact]
        public void TestAuthorizedItemFinished()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "");
            // Arrange
            Member tester = new Tester("Bob", "Test@example.com", "");
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestedState() };
            sprint.AddBacklogItem(backlogItem);
            sprint.AssignMembersToSprint(randomDeveloper);

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);
            sprint.AssignMembersToSprint(tester);

            backlogItem.Sprint!.SetNotificationStrategy(new TesterNotificationStrategy());
            sprint.AddSubscriber(new RegularSubscriber(tester));

            // Act
            backlogStateManager.FinishItem(randomDeveloper, backlogItem);

            // Assert
            Assert.IsType<FinishedState>(backlogItem.State);
            Assert.IsType<FinishedState>(activity.State);
            Assert.IsType<FinishedState>(activity1.State);
        }
    }
}
