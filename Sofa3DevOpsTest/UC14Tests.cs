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
            List<Visitor> visitors = new List<Visitor>()
            {
                new BuildVisitor(),
                new TestVisitor(),
                new AnalyseVisitor(),
                new DeploymentVisitor(),
                new OptionalVisitor()
            };
            BaseComposite = new CompositeComponent("Release pipeline 1");
            CICDPipeline = new Pipeline(BaseComposite, visitors);
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

        [Fact]
        public void TestPipelineWithFailedTest()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Perform unit test", "npm unit-test");
            Command integrationTest = new Command("Perform integration test", "npm integration-test");
            // Simulate failed command
            // A mock should throw error
            var mock = new Mock<IComponent>();
            mock.Setup((e) => e.AcceptVisitor(It.IsAny<Visitor>())).Throws(new Exception("Failed command"));
            IComponent failedCommand = mock.Object;

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);
            testStage.AddComponent(failedCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestPipelineWithFailedCodeAnalysis()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Perform unit test", "npm unit-test");
            Command integrationTest = new Command("Perform integration test", "npm integration-test");

            AnalyzeStage analysisStage = new AnalyzeStage("Perform code analysis");
            Command analysisCommand = new Command("Perform unit test", "npm sonarcloud-analysis");
            // Simulate failed command
            // A mock should throw error
            var mock = new Mock<IComponent>();
            mock.Setup((e) => e.AcceptVisitor(It.IsAny<Visitor>())).Throws(new Exception("Failed command"));
            IComponent failedCommand = mock.Object;

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);

            analysisStage.AddComponent(analysisCommand);
            analysisStage.AddComponent(failedCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);
            BaseComposite.AddComponent(analysisStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestPipelineWithFailedDeployment()
        {
            // Arrange
            BuildStage buildStage = new ("Build project");
            Command command = new ("Import packages", "npm Install");
            Command buildProject = new ("Import packages", "npm build");

            TestStage testStage = new ("Test project");
            Command testCommand = new ("Perform unit test", "npm unit-test");
            Command integrationTest = new ("Perform integration test", "npm integration-test");

            AnalyzeStage analysisStage = new ("Perform code analysis");
            Command analysisCommand = new ("Perform Sonar scan", "npm sonarcloud-analysis");

            DeploymentStage deployStage = new ("Deploy code");
            Command deployCommand = new ("Perform deployment to Railway", "upload deploy-railway");
            // Simulate failed command
            // A mock should throw error
            var mock = new Mock<IComponent>();
            mock.Setup((e) => e.AcceptVisitor(It.IsAny<Visitor>())).Throws(new Exception("Failed command"));
            IComponent failedCommand = mock.Object;

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);

            analysisStage.AddComponent(analysisCommand);

            deployStage.AddComponent(deployCommand);
            deployStage.AddComponent(failedCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);
            BaseComposite.AddComponent(analysisStage);
            BaseComposite.AddComponent(deployStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TestSuccesfullPipeline()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Perform unit test", "npm unit-test");
            Command integrationTest = new Command("Perform integration test", "npm integration-test");

            AnalyzeStage analysisStage = new AnalyzeStage("Perform code analysis");
            Command analysisCommand = new Command("Perform Sonar scan", "npm sonarcloud-analysis");

            DeploymentStage deployStage = new DeploymentStage("Deploy code");
            Command deployCommand = new Command("Perform deployment to Railway", "upload deploy-railway");

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);

            // Analysis
            analysisStage.AddComponent(analysisCommand);

            // Deployment
            deployStage.AddComponent(deployCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);
            BaseComposite.AddComponent(analysisStage);
            BaseComposite.AddComponent(deployStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestSuccesfullPipelineWithoutAnalysis()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Perform unit test", "npm unit-test");
            Command integrationTest = new Command("Perform integration test", "npm integration-test");

            DeploymentStage deployStage = new DeploymentStage("Deploy code");
            Command deployCommand = new Command("Perform deployment to Railway", "upload deploy-railway");

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);

            // Deployment
            deployStage.AddComponent(deployCommand);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);
            BaseComposite.AddComponent(deployStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestSuccesfullPipelineWithOptionalCommands()
        {
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Perform unit test", "npm unit-test");
            Command integrationTest = new Command("Perform integration test", "npm integration-test");

            DeploymentStage deployStage = new DeploymentStage("Deploy code");
            Command deployCommand = new Command("Perform deployment to Railway", "upload deploy-railway");
            
            OptionalStage optionalStage = new OptionalStage("Other commands");
            Command otherCommand = new Command("Check other service", "Echo 'Done'");
            Command otherCommand2 = new Command("Check other service", "Echo 'Done2'");

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Tests
            testStage.AddComponent(testCommand);
            testStage.AddComponent(integrationTest);

            // Deployment
            deployStage.AddComponent(deployCommand);

            // Optional stage 
            optionalStage.AddComponent(otherCommand);
            optionalStage.AddComponent(otherCommand2);

            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);
            BaseComposite.AddComponent(testStage);
            BaseComposite.AddComponent(deployStage);
            BaseComposite.AddComponent(optionalStage);

            // Act
            var result = Sprint.StartReleasePipeline(productOwner);

            // Assert
            Assert.True(result);
        }
    }
}
