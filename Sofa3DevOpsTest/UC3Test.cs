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
            DomainFactory factory = new DomainFactory();
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
            DomainFactory factory = new DomainFactory();
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
            DomainFactory factory = new DomainFactory();
            SprintStrategy strategy = new NonAuthorizedSprintStrategy();
            developer.SetSprintStrategy(strategy);

            //Act
            var sprint = Assert.Throws<UnauthorizedAccessException>(()=> developer.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day"));

            // Assert
            Assert.Equal("Does not have the right authorization to perform this action.", sprint.Message);
        }
    }
}
