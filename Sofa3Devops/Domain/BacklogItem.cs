using Sofa3Devops.BacklogStates;
using Sofa3Devops.Observers;

namespace Sofa3Devops.Domain
{
    public class BacklogItem : INotificationObservable
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public IBacklogState State { get; set; }
        public Member? ResponsibleMember { get; set; }
        public List<Subscriber> Subscribers { get; set; }
        public List<Activity> Activities { get; set; }
        public List<CommentThread> Threads { get; set; }

        public BacklogItem(string name, string description, IBacklogState state, Member? responsibleMember, List<Subscriber> subscribers, List<Activity> activities, List<CommentThread> threads)
        {
            Name = name;
            Description = description;
            State = state;
            ResponsibleMember = responsibleMember;
            Subscribers = subscribers;
            Activities = activities;
            Threads = threads;
        }

        public void AddSubscriber(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }

        public void NotifyAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveSubscriber(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }
    }
}