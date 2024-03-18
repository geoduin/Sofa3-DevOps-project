using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Observers;

namespace Sofa3Devops.Services
{
    public class ObservableServices
    {
        public static void AddSubscriberToDictionary(Subscriber subscriber, Dictionary<Type, List<Subscriber>> dictionary)
        {
            try
            {
                var typeList = dictionary[subscriber.NotifiedUser.GetType()];
                typeList.Add(subscriber);
            }
            catch
            {
                List<Subscriber> list = new List<Subscriber>()
                {
                    subscriber
                };
                dictionary.Add(subscriber.NotifiedUser.GetType(), list);
            }
        }

        public static void RemoveSubscriberFromDictionary(Subscriber subscriber,
            Dictionary<Type, List<Subscriber>> dictionary)
        {
            try
            {
                var typeList = dictionary[subscriber.NotifiedUser.GetType()];
                typeList.Remove(subscriber);
            }
            catch
            {
                throw new InvalidOperationException("Could not find subscriber");
            }
        }
    }
}
