using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;

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
        public INotificationStrategy? NotificationStrategy { get; private set; }
        

        public BacklogItem(string name, string description)
        {
            Name = name;
            Description = description;
            State = new TodoState();
            ResponsibleMember = null;
            Subscribers = new Dictionary<Type, List<Subscriber>>();
            Activities = new List<Activity>();
            Threads = new List<CommentThread>();
        }

        public virtual void AssignBacklogItem(Member member)
        {
            if (ResponsibleMember == null)
            {
                ResponsibleMember = member;
                SetToDoing();
                // Assign member as subscriber.
                // AddSubscriber(new Subscriber(member));
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
            var typeList = this.Subscribers[subscriber.GetType()];
            typeList.Add(subscriber);

        }

        public void NotifyAll(string title, string message)
        {
            this.NotificationStrategy.SendNotification(title, message, Subscribers);
        }

        public void RemoveSubscriber(Subscriber subscriber)
        {
            var list = this.Subscribers[subscriber.GetType()];
            list.Remove(subscriber);
        }

        public void SetNotificationStrategy(INotificationStrategy strategy)
        {
            this.NotificationStrategy = strategy;
        }

        public void SetBacklogState(IBacklogState backlogState)
        {
            State = backlogState;
        }

        public void SetToTodo()
        {
            State.SetToDo(this);
        }

        public void SetToDoing()
        {
            State.SetDoing(this);
        }

        public void SetToTesting()
        {
            State.SetToTesting(this);
        }

        public void SetItemReadyForTesting()
        {
            State.SetToReadyTesting(this);
        }
    }
}