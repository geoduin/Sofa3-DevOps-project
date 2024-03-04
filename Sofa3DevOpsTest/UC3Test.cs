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
    public class UC3Test
    {

        [Fact]
        public void TestScrumMasterSprintCreation()
        {
            // Arrange
            ScrumMaster scrumMaster = new ScrumMaster("Han");
            AbstractSprintFactory factory = new DevelopmentSprintFactory();
            SprintStrategy strategy = new AuthorizedSprintStrategy(factory);
            scrumMaster.SetSprintStrategy(strategy);

            // Act
            Sprint sprint = scrumMaster.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day");

            // Assert
            Assert.Equal("First sprint of the day", sprint.Name);
        }

        [Fact]
        public void TestProductOwnerSprintCreation() {
            // Arrange
            ProductOwner productOwner = new ProductOwner("Olaf");
            AbstractSprintFactory factory = new DevelopmentSprintFactory();
            SprintStrategy strategy = new AuthorizedSprintStrategy(factory);
            productOwner.SetSprintStrategy(strategy);

            // Act
            Sprint sprint = productOwner.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day");

            // Assert
            Assert.Equal("First sprint of the day", sprint.Name);
        }

        [Fact]
        public void TestUnAuthorisedSprintCreation() {
            // Arrange
            Developer developer = new Developer("Jonas");
            SprintStrategy strategy = new NonAuthorizedSprintStrategy();
            developer.SetSprintStrategy(strategy);

            //Act
            var sprint = Assert.Throws<UnauthorizedAccessException>(()=> developer.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day"));

            // Assert
            Assert.Equal("Does not have the right authorization to perform this action.", sprint.Message);
        }

        [Fact]
        public void TestAddMembersWithExtraScrumMaster()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 0");

            Member scrumMasterFirst = new ScrumMaster("Leader");
            Member scrumMasterSecond = new ScrumMaster("Interim-Leader");

            sprint.AssignMembersToSprint(scrumMasterFirst);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.AssignMembersToSprint(scrumMasterSecond));

            Assert.Equal("This sprint has already been assigned to a scrummaster", error.Message);
        }

        [Fact]
        public void TestAddMembersToSprintSuccesfully()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 0");

            Member scrumMaster = new ScrumMaster("Leader");
            Member developer = new Developer("Developer");
            Member tester = new Tester("Developer");

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester);

            Assert.NotNull(sprint.AssignScrumMaster);
            Assert.Equal("Leader", sprint.AssignScrumMaster.Name);
            Assert.Equal(2, sprint.Members.Count);
        }
    }
}
