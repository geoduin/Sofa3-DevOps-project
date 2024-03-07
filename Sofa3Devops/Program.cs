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
            BacklogItem item = new BacklogItem("test", "test");
            Sprint test = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "test");
            test.AddBacklogItem(item);
            item.Sprint = test;
            Member member = new Tester("test", "test", "test");
            Member member2 = new ProductOwner("test", "test", "test");

            item.State = new TestingState();
            test.Members.Add(member);
            test.Members.Add(member2);
            Subscriber memSubscriber = new RegularSubscriber(member);
            Subscriber poSub = new RegularSubscriber(member2);
            item.AddSubscriber(memSubscriber);
            item.AddSubscriber(poSub);
            item.SetToTested();
        }
    }
}
