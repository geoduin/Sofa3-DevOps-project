using DomainServices.DomainServicesImpl;
using Moq;
using Sofa3Devops.ComponentVisitors;
using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.ComponentVisitors.Visitors;
using Sofa3Devops.Domain;
using Sofa3Devops.Factories;
using Sofa3Devops.SprintStates;


namespace Sofa3DevOpsTest
{
    public class UC14Tests
    {
        private CompositeComponent BaseComposite {  get; set; }
        private Pipeline CICDPipeline {  get; set; }
        private SprintManager SprintManager { get; set; }
        private ReleaseSprint Sprint { get; set; }
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private ProductOwner productOwner { get; set; }

        public UC14Tests() {
            BaseComposite = new CompositeComponent("Release pipeline 1");
            CICDPipeline = new Pipeline(BaseComposite);
            Sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(3), "Release pipeline", CICDPipeline);
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster = new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            productOwner = new ProductOwner("Po", "PO@example.com", "PO");
            SprintManager = new SprintManager(new ReleaseSprintFactory());
            leadDeveloper.SetLeadDeveloper();
            Sprint.AssignMembersToSprint(tester);
            Sprint.AssignMembersToSprint(developer);
            Sprint.AssignMembersToSprint(scrumMaster);
            Sprint.AssignMembersToSprint(leadDeveloper);
            Sprint.AssignMembersToSprint(productOwner);
            Sprint.StartSprint();
        }


        [Fact]
        public void TestNonAuthorizedValidation()
        {
            var error = Assert.Throws<UnauthorizedAccessException>(()=> SprintManager.FinishSprint(Sprint, tester));

            Assert.Equal("Unauthorized action: Users with Tester role are not allowed to perform. Only scrum masters and Product owners.", error.Message);
        }

        [Fact]
        public void TestStartPipelineOfReleaseSprintInConceptState()
        {
            Sprint.State = new ConceptState();

            var error = Assert.Throws<InvalidOperationException>(() =>  SprintManager.FinishSprint(Sprint, scrumMaster));
            Assert.Equal("Cannot finish a sprint, without starting one.", error.Message);
        }

        [Fact]
        public void TestPipelineWithFailedBuild()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");
            // Simulate failed command
            // A mock should throw error
            var mock = new Mock<IComponent>();
            mock.Setup((e) => e.AcceptVisitor(It.IsAny<Visitor>())).Throws(new Exception());
            IComponent failedCommand = mock.Object;

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            buildStage.AddComponent(failedCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);

            // Act
            Sprint.EndSprint(productOwner);

            // Assert
            Assert.False(CICDPipeline.SuccesFlag);
        }

        [Fact]
        public void TestPipelineWithGreatBuild()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");
            // Simulate failed command
            // A mock should throw error

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.True(result);
        }

        [Fact(Skip = "")]
        public void TestPipelineWithFailedTest()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact(Skip = "")]
        public void TestPipelineWithFailedCodeAnalysis()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact(Skip = "")]
        public void TestPipelineWithFailedDeployment()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact(Skip ="")]
        public void TestSuccesfullPipeline()
        {
            // Arrange 
            // Act
            // Assert

        }
    }
}
