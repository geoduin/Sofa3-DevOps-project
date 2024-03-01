using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC5Test
    {
        [Fact]
        public void TestAddBacklogItemByScrumMaster()
        {
            ScrumMaster master = new ScrumMaster("George");
            Sprint sprint = new Sprint(DateTime.Now, DateTime.Now, "Sprint 1");
            BacklogItem backlogItem = new BacklogItem("Task 1", "NA");
            master.SetSprintStrategy(new AuthorizedSprintStrategy(new DomainFactory()));
            var result = master.AddBacklogItem(sprint, backlogItem);

            Assert.NotNull(result);
            Assert.Single(sprint.BacklogItems);
        }

        [Fact]
        public void TestAddBacklogItemByDeveloper()
        {
            ScrumMaster master = new ScrumMaster("George");
            Sprint sprint = new Sprint(DateTime.Now, DateTime.Now, "Sprint 1");
            BacklogItem backlogItem = new BacklogItem("Task 1", "NA");
            master.SetSprintStrategy(new NonAuthorizedSprintStrategy());
            
            var result = Assert.Throws<UnauthorizedAccessException>(()=> master.AddBacklogItem(sprint, backlogItem));

            Assert.Empty(sprint.BacklogItems);
            Assert.Equal("Does not have the right authorization to perform this action.", result.Message);
        }
    }
}
