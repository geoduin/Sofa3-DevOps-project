﻿using DomainServices.DomainServicesImpl;
using DomainServices.DomainServicesIntf;
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
    public class UC3Test
    {
        private readonly AbstractSprintFactory devSprintFactory;
        private readonly AbstractSprintFactory releaseSprintFactory;

        private readonly ISprintManager sprintManager;
        private readonly ISprintManager sprintManagerRelease;

        public UC3Test() {
            devSprintFactory = new DevelopmentSprintFactory();
            releaseSprintFactory = new ReleaseSprintFactory();
            sprintManager = new SprintManager(devSprintFactory);
            sprintManagerRelease = new SprintManager(releaseSprintFactory);
        }

        [Fact]
        public void TestScrumMasterSprintCreation()
        {
            // Arrange
            ScrumMaster productOwner = new ScrumMaster("Olaf", "dev@dev.nl", "dev@dev.nl");

            // Act
            Sprint sprint = sprintManager.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day", productOwner);

            // Assert
            Assert.Equal("First sprint of the day", sprint.Name);
        }

        [Fact]
        public void TestProductOwnerSprintCreation() {
            // Arrange
            ProductOwner productOwner = new ProductOwner("Olaf", "dev@dev.nl", "dev@dev.nl");

            // Act
            Sprint sprint = sprintManager.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day", productOwner);

            // Assert
            Assert.Equal("First sprint of the day", sprint.Name);
        }

        [Fact]
        public void TestUnAuthorisedSprintCreation() {
            // Arrange
            Developer developer = new Developer("Jonas", "dev@dev.nl", "dev@dev.nl");

            //Act
            var sprint = Assert.Throws<UnauthorizedAccessException>(()=> sprintManager.CreateSprint(DateTime.Now, DateTime.Now, "First sprint of the day", developer));

            // Assert
            Assert.Equal($"Unauthorized action: Users with {developer} role are not allowed to perform. Only scrum masters and Product owners.", sprint.Message);
        }

        [Fact]
        public void TestAddMembersWithExtraScrumMaster()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 0");

            Member scrumMasterFirst = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Member scrumMasterSecond = new ScrumMaster("Interim-Leader", "dev@dev.nl", "dev@dev.nl");

            sprint.AssignMembersToSprint(scrumMasterFirst);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.AssignMembersToSprint(scrumMasterSecond));

            Assert.Equal("This sprint has already been assigned to a scrummaster", error.Message);
        }

        [Fact]
        public void TestAddMembersToSprintSuccesfully()
        {
            Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.Now, "Sprint 0");

            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Member tester = new Tester("Developer", "dev@dev.nl", "dev2");
            Member developer = new Developer("Developer", "dev2@dev.nl", "dev2");
            

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester);

            Assert.NotNull(sprint.AssignScrumMaster);
            Assert.Equal("Leader", sprint.AssignScrumMaster.Name);
            Assert.Equal(2, sprint.Members.Count);
        }

        [Fact]
        public void StartSprintWithoutAuthority()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member tester = new Tester("D", "D@Outlook.com", "DSlack");
            
            var result = Assert.Throws<UnauthorizedAccessException>(()=> sprintManagerRelease.StartSprint(sprint, tester));

            Assert.Equal($"Unauthorized action: Users with {tester} role are not allowed to perform. Only scrum masters and Product owners.", result.Message);
        }

        [Fact]
        public void StartSprintWithAuthorityScrumMaster()
        {
            ProductOwner productOwner = new ProductOwner("Dave", "Dave", "");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member scrumMaster = new ScrumMaster("D", "D@Outlook.com", "DSlack");
            Developer developer = new Developer("Developer", "D@Outlook.com", "DSlack");
            Tester tester = new Tester("Developer", "D@Outlook.com", "DSlack");
            developer.SetLeadDeveloper();

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(productOwner);

            sprintManager.StartSprint(sprint, scrumMaster);

            Assert.IsType<OngoingState>(sprint.State);
        }

        [Fact]
        public void TestStartSprintWithoutScrumMaster()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Member developer = new Developer("Developer", "dev@dev.nl", "dev2");

            sprint.AssignMembersToSprint(developer);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());

            Assert.Equal("At least one tester, scrummaster, PO and developer must be added, before starting a sprint", error.Message);
        }

        [Fact]
        public void TestStartSprintWithoutTester()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Member tester = new Tester("Developer", "dev@dev.nl", "dev");
            Member developer = new Developer("Developer", "dev2@dev.nl", "dev2");
            

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());

            Assert.Equal("At least one tester, scrummaster, PO and developer must be added, before starting a sprint", error.Message);
        }

        [Fact]
        public void TestStartSprintWithoutLeadDeveloper()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Member developer = new Developer("Developer", "dev@dev.nl", "dev@dev.nl")
            {
                Seniority = false
            };
            Member tester = new Tester("Developer", "dev2@dev.nl", "dev2@dev.nl");

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());

            Assert.Equal("At least one tester, scrummaster, PO and developer must be added, before starting a sprint", error.Message);
        }

        [Fact]
        public void TestStartSprintWithoutPO()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Developer developer = new Developer("Developer", "dev@dev.nl", "dev@dev.nl");
            Developer developer2 = new Developer("Developer2", "dev2@dev.nl", "dev2@dev.nl");

            Tester tester = new Tester("Developer", "dev3@dev.nl", "dev3@dev.nl");
            Tester tester2 = new Tester("Developer", "dev4@dev.nl", "dev4@dev.nl");

            developer.SetLeadDeveloper();

            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester); sprint.AssignMembersToSprint(developer2);
            sprint.AssignMembersToSprint(tester2);

            var error = Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());

            Assert.Equal("At least one tester, scrummaster, PO and developer must be added, before starting a sprint", error.Message);
        }

        [Fact]
        public void TestStartSprintSuccesful()
        {
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Sprint");
            ProductOwner productOwner = new ProductOwner("Dave", "Dave", "");
            Member scrumMaster = new ScrumMaster("Leader", "leader@leader.nl", "leader");
            Developer developer = new Developer("Developer", "dev@dev.nl", "dev@dev.nl");
            Developer developer2 = new Developer("Developer2", "dev2@dev.nl", "dev2@dev.nl");
            
            Tester tester = new Tester("Developer", "dev3@dev.nl", "dev3@dev.nl");
            Tester tester2 = new Tester("Developer", "dev4@dev.nl", "dev4@dev.nl");

            developer.SetLeadDeveloper();
           
            sprint.AssignMembersToSprint(scrumMaster);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(productOwner);
            
            sprint.AssignMembersToSprint(developer2);
            sprint.AssignMembersToSprint(tester2);

            sprint.StartSprint();

            Assert.IsType<OngoingState>(sprint.State);
        }
    }
}
