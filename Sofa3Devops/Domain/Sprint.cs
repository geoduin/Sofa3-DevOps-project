using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.Services;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public abstract class Sprint : INotificationObservable
    {
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public string Name { get; protected set; }
        public ISprintState State { get; set; }
        public SprintReport? SprintReport { get; set; }
        public Pipeline? PublishingPipeline { get; set; }
        public Member AssignScrumMaster {  get; set; }
        public List<BacklogItem> BacklogItems { get; set; }
        public List<Member> Members { get; set; }
        public Dictionary<Type, List<Subscriber>> Subscribers { get; protected set; }
        public INotificationStrategy NotificationStrategy { get; protected set; }



        public Sprint(DateTime startDate, DateTime endDate, string name)
        {
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
            State = new ConceptState();
            PublishingPipeline = null;
            BacklogItems = new List<BacklogItem>();
            Members = new List<Member>();
            Subscribers = new Dictionary<Type, List<Subscriber>>();
        }

        public void SetSprintState(ISprintState sprintState)
        {
            State = sprintState;
        }

        public void AddBacklogItem(BacklogItem item)
        {
            item.Sprint = this;
            BacklogItems.Add(item);
        }

        public abstract void ChangeSprint(DateTime newStart, DateTime newEnd, string newName);

        public void AssignMembersToSprint(Member member)
        {
            if(member.GetType() == typeof(ScrumMaster))
            {
                AssignScrumMasterToSprint(member);
            }
            else
            {
                Members.Add(member);
            }
        }

        private void AssignScrumMasterToSprint(Member member)
        {
            if (AssignScrumMaster == null)
            {
                AssignScrumMaster = member;
            }
            else
            {
                throw new InvalidOperationException("This sprint has already been assigned to a scrummaster");
            }
        }
    
        public void StartSprint()
        {
            State.SetToOngoing(this);
        }

        public void CancelSprint()
        {
            State.SetToCanceled(this);
        }

        public void FinishSprint(Member member)
        {
            State.SetToFinished(this);
            EndSprint(member);
        }

        // Will be called upon in the states
        public abstract void EndSprint(Member member);

        public void AddSubscriber(Subscriber subscriber)
        {
            try
            {
                var typeList = this.Subscribers[subscriber.NotifiedUser.GetType()];
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

        public void RemoveSubscriber(Subscriber subscriber)
        {
            var list = this.Subscribers[subscriber.GetType()];
            list.Remove(subscriber);
        }

        public abstract void NotifyAll(string title, string message);

        public void SetNotificationStrategy(INotificationStrategy strategy)
        {
            this.NotificationStrategy = strategy;
        }
    }
}
