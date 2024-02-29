using Sofa3Devops.Domain;

namespace Sofa3DevOpsTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void TestAssignmentOfBacklogItem()
        {
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description");
            Developer developer = new Developer();

            backlogItem.AssignBacklogItem(developer);

            Assert.NotNull(developer);
            Assert.Equal(developer, backlogItem.ResponsibleMember);
        }

        [Fact]
        public void TestAssignmentOfBacklogItemAlreadyTaken() 
        {
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description");
            Developer developer = new Developer();
            Developer existingDeveloper = new Developer();
            backlogItem.AssignBacklogItem(existingDeveloper); 
           
            var result = Assert.Throws<InvalidOperationException>(() => backlogItem.AssignBacklogItem(developer));

            Assert.NotNull(developer);
            Assert.NotEqual(developer, backlogItem.ResponsibleMember);
            Assert.Equal("Backlog item has already been assigned to a member", result.Message);
        }

        [Fact]
        public void TestUnassignBacklogItem()
        {

            Developer developer = new Developer();
            BacklogItem backlogItem = new BacklogItem("Task 1", "Test description")
            {
                ResponsibleMember = developer
            };

            backlogItem.UnAssignBacklogItem();

            Assert.Null(backlogItem.ResponsibleMember);
        }
    }
}