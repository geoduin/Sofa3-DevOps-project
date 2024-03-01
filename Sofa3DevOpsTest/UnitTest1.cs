using Sofa3Devops.Domain;

namespace Sofa3DevOpsTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestAssignmentOfBacklogItem()
        {
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description");
            Developer developer = new Developer("Bob");

            backlogItem.AssignBacklogItem(developer);

            Assert.NotNull(developer);
            Assert.Equal(developer, backlogItem.ResponsibleMember);
        }

        [Fact]
        public void TestAssignmentOfBacklogItemAlreadyTaken() 
        {
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description");
            Developer developer = new Developer("Bob");
            Developer existingDeveloper = new Developer("Arnold");
            backlogItem.AssignBacklogItem(existingDeveloper); 
           
            var result = Assert.Throws<InvalidOperationException>(() => backlogItem.AssignBacklogItem(developer));

            Assert.NotNull(developer);
            Assert.NotEqual(developer, backlogItem.ResponsibleMember);
            Assert.Equal("Backlog item has already been assigned to a member", result.Message);
        }

        [Fact]
        public void TestUnassignBacklogItem()
        {

            Developer developer = new Developer("Bob");
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description")
            {
                ResponsibleMember = developer
            };

            backlogItem.UnAssignBacklogItem();

            Assert.Null(backlogItem.ResponsibleMember);
        }

        [Fact]
        public void TestAddActivityToBacklogItem()
        {
            Developer developer = new Developer("Bob");
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description")
            {
                ResponsibleMember = developer
            };

            Activity activity = new Activity("SubTask1", "Sub description", backlogItem);
            Activity activity2 = new Activity("SubTask2", "Sub description2", backlogItem);

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity2);

            Assert.Equal(2, backlogItem.Activities.Count);
        }

        [Fact]
        public void TestAddActivityWithExistingItem()
        {
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description");
            BacklogItem dummyItem = new BacklogItem("Task 1", "Test description");

            Activity activity = new Activity("SubTask1", "Sub description", backlogItem);
            Activity alreadyAssignedBacklogItem = new Activity("SubTask2", "Sub description2", dummyItem);

            backlogItem.AddActivityToBacklogItem(activity);
            var error = Assert.Throws<InvalidOperationException>(()=> backlogItem.AddActivityToBacklogItem(alreadyAssignedBacklogItem));

            Assert.Single(backlogItem.Activities);
            Assert.Equal("Cannot assign activities with existing backlog-item assigned.", error.Message);
        }
    }
}