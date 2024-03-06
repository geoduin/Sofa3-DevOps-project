using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC12Tests
    {
        private Member tester { get; set; }
        private Member developer { get; set; }
        private Member scrumMaster { get; set; }
        private Developer leadDeveloper { get; set; }
        private BacklogItem backlogItem1 { get; set; }
        private BacklogItem backlogItem2 { get; set; }
        private Sprint sprint { get; set; }

        public UC12Tests()
        {
            tester = new Tester("Test", "Test@example.com", "TestSlack");
            developer = new Developer("Developer", "Developer@example.com", "DevSlack");
            scrumMaster = new ScrumMaster("Master", "Master@example.com", "MasterSlack");
            leadDeveloper = new Developer("LeadDeveloper", "LeadDeveloper@example.com", "DevSlack");
            leadDeveloper.SetLeadDeveloper();
            backlogItem1 = new BacklogItem("Head task", "")
            {
                State = new ReadyToTestingState(),
                ResponsibleMember = developer
            };
            backlogItem2 = new BacklogItem("Head task", "")
            {
                State = new TestingState(),
                ResponsibleMember = developer
            };
            sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(1), "Scrum project")
            {
                State = new OngoingState()
            };
            var leadDevSubscriber = new RegularSubscriber(leadDeveloper);
            var devSubscriber = new RegularSubscriber(developer);
            var testerSubscriber = new RegularSubscriber(tester);
            var scrumMasterSubscriber = new RegularSubscriber(scrumMaster);

            sprint.AssignMembersToSprint(tester);
            sprint.AssignMembersToSprint(developer);
            sprint.AssignMembersToSprint(leadDeveloper);
            sprint.AssignMembersToSprint(scrumMaster);

            sprint.AddSubscriber(testerSubscriber);
            sprint.AddSubscriber(leadDevSubscriber);
            sprint.AddSubscriber(devSubscriber); 
            sprint.AddSubscriber(scrumMasterSubscriber);
        }

        [Fact]
        public void TestIfSubscribersArePresent()
        {
            var dick = sprint.Subscribers;

            //Act
            var result = dick.GetValueOrDefault(typeof(Tester));
            var resultDevelopers = dick.GetValueOrDefault(typeof(Developer));
            var resultScrumMaster = dick.GetValueOrDefault(typeof(ScrumMaster));

            Assert.Single(result!);
            Assert.Equal(2, resultDevelopers!.Count);
            Assert.Single(resultScrumMaster!);
        }
    }
}
