using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintStates;

namespace Sofa3DevOpsTest
{
    public class NotificationTest
    {
        [Fact]
        public void AllNotificationShouldSendNotificationToAllMembers()
        {
            var test1Subscriber = new Mock<Subscriber>(new Tester("test", "test", "test"));
            var dev1Subscriber = new Mock<Subscriber>(new Developer("dev", "dev", "dev"));
            var sm1Subscriber = new Mock<Subscriber>(new ScrumMaster("sm", "sm", "sm"));
            var po1Subscriber = new Mock<Subscriber>(new ProductOwner("po", "po", "po"));

            Dictionary<Type, List<Subscriber>> subDictionary = new Dictionary<Type, List<Subscriber>>
            {
                { typeof(Tester), new List<Subscriber>() },
                { typeof(Developer), new List<Subscriber>() },
                { typeof(ScrumMaster), new List<Subscriber>() },
                { typeof(ProductOwner), new List<Subscriber>() }
            };
            subDictionary[typeof(Tester)].Add(test1Subscriber.Object);
            subDictionary[typeof(Developer)].Add(dev1Subscriber.Object);
            subDictionary[typeof(ScrumMaster)].Add(sm1Subscriber.Object);
            subDictionary[typeof(ProductOwner)].Add(po1Subscriber.Object);
            INotificationStrategy testStrategy = new AllNotificationStrategy();
            testStrategy.SendNotification("test", "test", subDictionary);
            test1Subscriber.Verify(t => t.Notify("test", "test"), Times.Exactly(1));
            dev1Subscriber.Verify(t => t.Notify("test", "test"), Times.Exactly(1));
            sm1Subscriber.Verify(t => t.Notify("test", "test"), Times.Exactly(1));
            po1Subscriber.Verify(t => t.Notify("test", "test"), Times.Exactly(1));

        }


        [Fact]
        public void SettingSprintToCancelledShouldTriggerNotificationIfInitialStateIsOngoingAndSprintTypeIsReleaseSprint()
        {
            var notificationMock = new Mock<INotificationStrategy>();
            Sprint testSprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "test");
            testSprint.SetNotificationStrategy(notificationMock.Object);
            testSprint.State = new OngoingState();
            testSprint.State.SetToCanceled(testSprint);
            notificationMock.Verify(c => c.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<Type, List<Subscriber>>>()), Times.Exactly(1));

        }

        [Fact]
        public void SettingSprintToCancelledShouldNotTriggerNotificationIfSprintTypeIsDevelopmentSprint()
        {
            var notificationMock = new Mock<INotificationStrategy>();
            Sprint testSprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            testSprint.SetNotificationStrategy(notificationMock.Object);
            testSprint.State = new OngoingState();
            testSprint.State.SetToCanceled(testSprint);
            notificationMock.Verify(c => c.SendNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<Type, List<Subscriber>>>()), Times.Exactly(0));
        }
    }
}
