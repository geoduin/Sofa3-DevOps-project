using DomainServices.DomainServicesImpl;
using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC15Tests
    {
        [Fact]
        public void TestFinishScrumSprintWithoutSummaryUploaded()
        {
            // Arrange
            SprintManager sprintManager = new SprintManager(new DevelopmentSprintFactory());
            ScrumMaster scrumMaster = new ScrumMaster("", "", "");
            Sprint developmentSprint = sprintManager.CreateSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum sprint", scrumMaster);

            developmentSprint.State = new OngoingState();

            // Act
            var error = Assert.Throws<InvalidOperationException>(() => developmentSprint.FinishSprint(scrumMaster));

            Assert.Equal("Summary is required in order to end the review sprint.", error.Message);
        }

        [Fact]
        public void TestFinishScrumSprintWithSummaryUploaded()
        {
            // Arrange
            SprintManager sprintManager = new SprintManager(new DevelopmentSprintFactory());
            ScrumMaster scrumMaster = new ScrumMaster("", "", "");
            DevelopmentSprint developmentSprint = (DevelopmentSprint) sprintManager.CreateSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum sprint", scrumMaster);
            List<string> notes = new List<string>()
            {
                "Sprint went well",
                "Implementation could be better."
            };
            developmentSprint.State = new OngoingState();
            developmentSprint.Summary = new SprintSummary("Sprint review of january", notes);
            // Act
            developmentSprint.FinishSprint(scrumMaster);

            var contentPrintedOut = developmentSprint.Summary.ToString();
            Assert.IsType<FinishedState>(developmentSprint.State);
            Assert.Equal("Sprint review of january: Sprint went well, Implementation could be better., ", contentPrintedOut);
        }
    }
}
