using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;
using Sofa3Devops.Observers;

namespace Sofa3Devops.Services
{
    public class SubscriberServices
    {
        public static void InitializeSubscriberDictionary(Dictionary<Type, List<Subscriber>> dictionary)
        {
            dictionary.Add(typeof(Developer), new List<Subscriber>());
            dictionary.Add(typeof(ProductOwner), new List<Subscriber>());
            dictionary.Add(typeof(ScrumMaster), new List<Subscriber>());
            dictionary.Add(typeof(Tester), new List<Subscriber>());
        }
    }
}
