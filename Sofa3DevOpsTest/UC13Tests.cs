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

        public UC13Tests() {
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project");
        }
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
        public void TestBacklogItemTestedToTestedValid()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            backlogItem.SetNotificationStrategy(new AllNotificationStrategy());
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestedState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            backlogItem.SetItemToFinished();

            Assert.IsType<FinishedState>(backlogItem.State);
        }

        [Fact]
        public void TestBacklogItemInvalidTestedToTested()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            Activity activity = new Activity("", "", backlogItem) { State = new TestedState() };
            Activity activity1 = new Activity("", "", backlogItem) { State = new TestingState() };

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity1);

            var error = Assert.Throws<InvalidOperationException>(()=> backlogItem.SetItemToFinished());

            Assert.IsNotType<FinishedState>(backlogItem.State);
            Assert.Equal("All tasks and subtasks need to be completed, before finishing this backlog item", error.Message);
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

        [Fact]
        public void TestUnAuthorizedItemFinished()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };

            Member randomDeveloper = new Developer("Dic", "", "");
            Member quackDeveloper = new Developer("Rick", "", "");
            Member daveDeveloper = new ScrumMaster("Dave", "", "");


            var error1 = Assert.Throws<UnauthorizedAccessException>(() => randomDeveloper.ApproveAndFinishItem(backlogItem));
            var error2 = Assert.Throws<UnauthorizedAccessException>(() => quackDeveloper.ApproveAndFinishItem(backlogItem));
            var error3 = Assert.Throws<UnauthorizedAccessException>(() => daveDeveloper.ApproveAndFinishItem(backlogItem));

            var ExpectedErrorMessage = "Does not have authority to Finish the backlog item. Only lead developers are allowed to do that.";
            Assert.Equal(ExpectedErrorMessage, error1.Message);
            Assert.Equal(ExpectedErrorMessage, error2.Message);
            Assert.Equal(ExpectedErrorMessage, error2.Message);
        }

        [Fact]
        public void TestUnAuthorizedItemDisapprove()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };

            Member randomDeveloper = new Developer("Dic", "", "");
            Member quackDeveloper = new Developer("Rick", "", "");
            Member daveDeveloper = new ScrumMaster("Dave", "", "");


            var error1 = Assert.Throws<UnauthorizedAccessException>(() => randomDeveloper.DisapproveTestedItem(backlogItem));
            var error2 = Assert.Throws<UnauthorizedAccessException>(() => quackDeveloper.DisapproveTestedItem(backlogItem));
            var error3 = Assert.Throws<UnauthorizedAccessException>(() => daveDeveloper.DisapproveTestedItem(backlogItem));

            var ExpectedErrorMessage = "Does not have authority to disapprove the backlog item. Only lead developers are allowed to do that.";
            Assert.Equal(ExpectedErrorMessage, error1.Message);
            Assert.Equal(ExpectedErrorMessage, error2.Message);
            Assert.Equal(ExpectedErrorMessage, error2.Message);
        }

        [Fact]
        public void TestAuthorizedItemDisapprove()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            backlogItem.SetNotificationStrategy(new TesterNotificationStrategy());
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            Tester tester = new Tester("Bob", "Test@example.com", "");
            backlogItem.AddSubscriber(new RegularSubscriber(tester));
            sprint.AddBacklogItem(backlogItem);

            randomDeveloper.DisapproveTestedItem(backlogItem);

            Assert.IsType<ReadyToTestingState>(backlogItem.State);
        }

        [Fact]
        public void TestAuthorizedItemFinished()
        {
            // Arrange
            Member tester = new Tester("Bob", "Test@example.com", "");
            Developer randomDeveloper = new Developer("Dic", "", "");
            randomDeveloper.SetLeadDeveloper();
            BacklogItem backlogItem = new BacklogItem("Task1", "Description")
            {
                State = new TestedState()
            };
            backlogItem.SetNotificationStrategy(new TesterNotificationStrategy());
            backlogItem.AddSubscriber(new RegularSubscriber(tester));

            // Act
            randomDeveloper.ApproveAndFinishItem(backlogItem);

            // Assert
            Assert.IsType<FinishedState>(backlogItem.State);
        }
    }
}
