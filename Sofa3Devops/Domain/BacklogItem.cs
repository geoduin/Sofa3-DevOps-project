using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.Services;

namespace Sofa3Devops.Domain
{
    public class BacklogItem : INotificationObservable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public IBacklogState State { get; set; }
        public Member? ResponsibleMember { get; set; }
        public Dictionary<Type, List<Subscriber>> Subscribers { get; private set; }
        public List<Activity> Activities { get; set; }
        public List<CommentThread> Threads { get; set; }
        public Sprint? Sprint { get; set; }
        public INotificationStrategy NotificationStrategy { get; private set; }
        

        public BacklogItem(string name, string description)
        {
            Name = name;
            Description = description;
            State = new TodoState();
            ResponsibleMember = null;
            Subscribers = new Dictionary<Type, List<Subscriber>>();
            SubscriberServices.InitializeSubscriberDictionary(Subscribers);
            Activities = new List<Activity>();
            Threads = new List<CommentThread>();
        }

        public void AssignBacklogItem(Member member)
        {
            if (ResponsibleMember == null)
            {
                ResponsibleMember = member;
            }
            else
            {
                throw new InvalidOperationException("Backlog item has already been assigned to a member");
            }
        }

        public void UnAssignBacklogItem() {
            ResponsibleMember = null;
        }

        public void AddActivityToBacklogItem(Activity activity)
        {
            // Check if activity is the same as it is assigned
            if (Equals(activity.AssignedBacklogItem))
            {
                Activities.Add(activity);
            }
            else
            {
                // If it is, throw error. Otherwise
                throw new InvalidOperationException("Cannot assign activities with existing backlog-item assigned.");
            }
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            var typeList = this.Subscribers[subscriber.NotifiedUser.GetType()];
            typeList.Add(subscriber);

        }

        public void NotifyAll(string title, string message)
        {
            this.NotificationStrategy.SendNotification(title, message, Subscribers);
        }

        public void RemoveSubscriber(Subscriber subscriber)
        {
            var list = this.Subscribers[subscriber.NotifiedUser.GetType()];
            list.Remove(subscriber);
        }

        public void SetNotificationStrategy(INotificationStrategy strategy)
        {
            this.NotificationStrategy = strategy;
        }

        public void SetToTested(Member tester)
        {
            if (tester.GetType().Equals(typeof(Tester)) && this.Sprint.Members.Contains(tester))
            {
                this.State.SetToTested(this);
            }
            else
            {
                throw new UnauthorizedAccessException(
                    "Only testers that are members of the sprint can set backlog items to tested");
            }
            
        }

        public void SetToFinished(Member productOwner)
        {
            if (productOwner.GetType().Equals(typeof(ProductOwner)) && this.Sprint.Members.Contains(productOwner))
            {
                this.State.SetToFinished(this);
            }
            else
            {
                throw new UnauthorizedAccessException(
                    "Only Product Owners that are members of the sprint can set backlog items to finished");
            }
        }

        public void SetToToDo(Member productOwner)
        {
            if (productOwner.GetType().Equals(typeof(ProductOwner)) && this.Sprint.Members.Contains(productOwner))
            {
                this.State.SetToDo(this);
            }
            else
            {
                throw new UnauthorizedAccessException(
                    "Only Product Owners that are members of the sprint can set backlog items to to-do");
            }
        }

    }
}