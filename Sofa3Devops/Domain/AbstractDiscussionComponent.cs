using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.Services;

namespace Sofa3Devops.Domain
{
    public abstract class AbstractDiscussionComponent : INotificationObservable
    {

        public string Title { get; set;  }
        public string Content { get; set;  }
        public BacklogItem RelevantItem { get; }
        public Member Poster { get; }
        public List<AbstractDiscussionComponent> Children { get; }
        private Dictionary<Type, List<Subscriber>> Subscribers;
        //This way we initially have the all notification strategy which is needed for the current acceptance criteria but
        //can still change the strategy if this becomes necessary in the future.
        public INotificationStrategy NotificationStrategy { get; set; } = new AllNotificationStrategy();

        protected AbstractDiscussionComponent(string title, string content, BacklogItem relevantItem, Member poster)
        {
            
                Title = title;
                Content = content;
                RelevantItem = relevantItem;
                Poster = poster;
                this.Children = new List<AbstractDiscussionComponent>();
                this.Subscribers = new Dictionary<Type, List<Subscriber>>();
                //TODO: Bespreken of dit beter met een factory of method parameter kan worden gedaan
                this.AddSubscriber(new RegularSubscriber(poster));
        }


        public abstract void AddComponent(AbstractDiscussionComponent component);
        public abstract void RemoveComponent(AbstractDiscussionComponent component);
        public abstract AbstractDiscussionComponent GetParent();
        public abstract void SetParent(DiscussionComment component);

        public void AddSubscriber(Subscriber subscriber)
        {
            ObservableServices.AddSubscriberToDictionary(subscriber, Subscribers);
        }

        public void RemoveSubscriber(Subscriber subscriber)
        {
            ObservableServices.RemoveSubscriberFromDictionary(subscriber, Subscribers);
        }

        public void NotifyAll(string title, string message)
        {
            NotificationStrategy.SendNotification(title, message, Subscribers);
        }
    }
}
