using System.Runtime.CompilerServices;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductOwner productOwner = new ProductOwner("Po", "PO@example.com", "PO");
            CompositeComponent BaseComposite = new CompositeComponent("Release pipeline 1");
            Pipeline CICDPipeline = new Pipeline(BaseComposite);
            ReleaseSprint Sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(3), "Release pipeline", CICDPipeline);
            
            
            // Arrange
            BuildStage buildStage = new BuildStage("Build project");
            Command command = new Command("Import packages", "npm Install");
            Command buildProject = new Command("Import packages", "npm build");

            TestStage testStage = new TestStage("Test project");
            Command testCommand = new Command("Import packages", "npm test");
            // Simulate failed command
            // A mock should throw error

            // Commands for build
            buildStage.AddComponent(command);
            buildStage.AddComponent(buildProject);
            // Add build stage to pipeline
            BaseComposite.AddComponent(buildStage);

            testStage.AddComponent(testCommand);
            BaseComposite.AddComponent(testStage);
            // Act
            Sprint.StartReleasePipeline(productOwner);
        }
    }
}
