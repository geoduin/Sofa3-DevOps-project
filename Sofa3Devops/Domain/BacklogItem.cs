﻿using Sofa3Devops.BacklogStates;
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

        public BacklogItem(string name, string description)
        {
            Name = name;
            Description = description;
            State = new TodoState();
            ResponsibleMember = null;
            Subscribers = new List<Subscriber>();
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