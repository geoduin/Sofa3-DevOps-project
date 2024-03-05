using System.Runtime.CompilerServices;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
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

            Sprint testSprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "test sprint");
            Member scrumMaster = new ScrumMaster("Scrum Master", "smaster@test.nl", "smaster");
            scrumMaster.WayToNotify = new EmailAdapter();
            Subscriber smSub = new RegularSubscriber(scrumMaster);
            Member dev = new Developer("Ingrid", "dev@test.nl", "dev");
            dev.WayToNotify = new SlackAdapter();
            Subscriber devSub = new RegularSubscriber(dev);
            Member tester = new Tester("tester", "tester@test.nl", "tester");
            tester.WayToNotify = new EmailAdapter();
            Subscriber testSub = new RegularSubscriber(tester);
            testSprint.AddSubscriber(smSub);
            testSprint.AddSubscriber(devSub);
            testSprint.AddSubscriber(testSub);
            testSprint.State = new OngoingState();
            testSprint.SetNotificationStrategy(new AllNotificationStrategy());
            testSprint.State.SetToCanceled(testSprint);



        }
    }
}
