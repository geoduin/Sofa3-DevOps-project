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
    public class UC13Tests
    {

        [Fact]
        public void TestSingleBacklogTest()
        {
            
            Developer developer = new Developer("Bob", "", "");
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

            var result = activity1.HasAllTaskBeenCompleted();

            Assert.True(result);
        }
    }
}
