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
                SetToDoing(member);
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
                activity.Sprint = Sprint;
                Activities.Add(activity);
            }
            else
            {
                // If it is, throw error. Otherwise
                throw new InvalidOperationException("Cannot assign activities with existing backlog-item assigned.");
            }
        }

        public virtual bool HasAllTaskBeenCompleted()
        {
            // Tests if the state of this object is tested.
            bool IsItemTested = State.GetType() == typeof(TestedState);
            // If array is empty or all activities are in TestedState, this value should always return true.
            bool AreActivitiesTested = Activities.TrueForAll(x => x.State.GetType() == typeof(TestedState)) || Activities.Count == 0;
            return IsItemTested && AreActivitiesTested;
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            try
            {
                var typeList = Subscribers[subscriber.NotifiedUser.GetType()];
                typeList.Add(subscriber);
            }
            catch
            {
                List<Subscriber> list = new List<Subscriber>()
                {
                    subscriber
                };
                Subscribers.Add(subscriber.NotifiedUser.GetType(), list);
            }

        }

        public void NotifyAll(string title, string message)
        {
            Sprint!.NotifyAll(title, message);
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
        
        public void SetToToDo(Member member)
        {
            if (member.GetType().Equals(typeof(Tester)) && this.Sprint!.Members.Contains(member))
            {
                this.State.SetToDo(this, member);
                return;
            }
            throw new UnauthorizedAccessException(
                "Only Testers that are members of the sprint can set backlog items to to-do");
        }

        public void SetBacklogState(IBacklogState backlogState)
        {
            State = backlogState;
        }

        public void SetToTodo(Member member)
        {
            State.SetToDo(this, member);
        }

        public void SetItemReadyForTesting(Member member)
        {
            State.SetToReadyTesting(this, member);
        }

        public void SetToDoing(Member member)
        {
            State.SetDoing(this, member);
        }

        public void SetToTesting(Member member)
        {
            State.SetToTesting(this, member);
        }

        public void SetToTested(Member member)
        {
            State.SetToTested(this, member);
        }

        public void SetItemToFinished(Member member)
        {
            State.SetToFinished(this, member);
        }
    }
}