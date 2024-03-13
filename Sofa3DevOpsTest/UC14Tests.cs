using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC14Tests
    {
        private Pipeline? CICDPipeline {  get; set; }
        private Sprint Sprint { get; set; }
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private ProductOwner productOwner { get; set; }

        public UC14Tests() {
            Sprint = new ReleaseSprint(DateTime.Now, DateTime.Now.AddDays(3), "Release pipeline");
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster = new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            productOwner = new ProductOwner("Po", "PO@example.com", "PO");
            leadDeveloper.SetLeadDeveloper();
            Sprint.AssignMembersToSprint(tester);
            Sprint.AssignMembersToSprint(developer);
            Sprint.AssignMembersToSprint(scrumMaster);
            Sprint.AssignMembersToSprint(leadDeveloper);
            Sprint.AssignMembersToSprint(productOwner);
            Sprint.StartSprint();
        }


        [Fact]
        public void TestNonScrumMasterValidation()
        {

        }

        [Fact]
        public void TestNonPOValidation()
        {

        }

        [Fact]
        public void TestPipelineWithFailedBuild()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact]
        public void TestPipelineWithFailedTest()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact]
        public void TestPipelineWithFailedCodeAnalysis()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact]
        public void TestPipelineWithFailedDeployment()
        {
            // Arrange 
            // Act
            // Assert

        }

        [Fact]
        public void TestSuccesfullPipeline()
        {
            // Arrange 
            // Act
            // Assert

        }
    }
}
