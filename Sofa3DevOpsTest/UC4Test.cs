using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStates;
using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
using System.Diagnostics.Metrics;

namespace Sofa3DevOpsTest
{
    public class UC4Test
    {
        private readonly AbstractSprintFactory devSprintFactory;
        private readonly AbstractSprintFactory releaseSprintFactory;

        private readonly ISprintManager sprintManager;
        private readonly ISprintManager sprintManagerRelease;

        public UC4Test()
        {
            devSprintFactory = new DevelopmentSprintFactory();
            releaseSprintFactory = new ReleaseSprintFactory();
            sprintManager = new SprintManager(devSprintFactory);
            sprintManagerRelease = new SprintManager(releaseSprintFactory);
        }


        [Fact]
        public void TestSprintCancelation()
        {
            ScrumMaster scrumMaster = new ScrumMaster("Scrum", "D@Outlook.com", "DSlack");
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint canceled");

            sprintManager.CancelSprint(sprint, scrumMaster);

            Assert.IsType<CanceledState>(sprint.State);
        }

        [Fact]
        public void TestSprintCancelingNonAuthorised() {
            Developer dev = new Developer("Scrum", "D@Outlook.com", "DSlack");
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint canceled");

            var error = Assert.Throws<UnauthorizedAccessException>(()=> sprintManager.CancelSprint(sprint, dev));

            Assert.Equal($"Unauthorized action: Users with {dev} role are not allowed to set item to testing. Only testers are allowed to move backlog-item to Testing.", error.Message);
        }
    }
}
