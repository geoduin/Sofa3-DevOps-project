using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;
using Sofa3Devops.Observers;
using Sofa3Devops.Services;

namespace Sofa3DevOpsTest
{
    public class ObservableServiceTest
    {
        [Fact]
        public void AddSubscriberToDictionaryAddsSubscriberToDictionary()
        {
            Dictionary<Type, List<Subscriber>> dictionary = new Dictionary<Type, List<Subscriber>>();
            Member member = new ScrumMaster("test", "test", "test");
            Subscriber sub = new RegularSubscriber(member);
            dictionary.Add(member.GetType(), new List<Subscriber>());
            ObservableServices.AddSubscriberToDictionary(sub, dictionary);

            Assert.True(dictionary[member.GetType()].Count == 1);

        }

        [Fact]
        public void AddSubscriberToDictionaryAddsListIfListDoesNotExist()
        {
            Dictionary<Type, List<Subscriber>> dictionary = new Dictionary<Type, List<Subscriber>>();
            Member member = new ScrumMaster("test", "test", "test");
            Subscriber sub = new RegularSubscriber(member);
            ObservableServices.AddSubscriberToDictionary(sub, dictionary);
            Assert.True(dictionary[member.GetType()].Count == 1);
        }

        public void RemoveSubscriberFromDictionaryRemovesSubscriberFromDictionary()
        {
            Dictionary<Type, List<Subscriber>> dictionary = new Dictionary<Type, List<Subscriber>>();
            Member member = new ScrumMaster("test", "test", "test");
            Subscriber sub = new RegularSubscriber(member);
            List<Subscriber> list = new List<Subscriber>();
            list.Add(sub);
            dictionary.Add(member.GetType(), list);
            ObservableServices.RemoveSubscriberFromDictionary(sub, dictionary);

            Assert.True(dictionary[member.GetType()].Count == 0);

        }

        [Fact]
        public void RemoveSubscriberFromDictionaryDoesNotRemoveSubscriberIfSubscriberNotInList()
        {
            Dictionary<Type, List<Subscriber>> dictionary = new Dictionary<Type, List<Subscriber>>();
            Member member = new ScrumMaster("test", "test", "test");
            Member member2 = new ScrumMaster("test2", "test2", "test2");
            Subscriber sub = new RegularSubscriber(member);
            Subscriber differentSub = new RegularSubscriber(member2);
            List<Subscriber> list = new List<Subscriber>();
            list.Add(sub);
            dictionary.Add(member.GetType(), list);
            ObservableServices.RemoveSubscriberFromDictionary(differentSub, dictionary);

            Assert.True(dictionary[member.GetType()].Count == 1);

        }

        [Fact]
        public void RemoveSubscriberFromDictionaryCreatesNewListIfTypeDoesNotHaveList()
        {
            Dictionary<Type, List<Subscriber>> dictionary = new Dictionary<Type, List<Subscriber>>();
            Member member = new ScrumMaster("test", "test", "test");
            Member member2 = new Tester("test2", "test2", "test2");
            Subscriber sub = new RegularSubscriber(member);
            Subscriber differentSub = new RegularSubscriber(member2);
            List<Subscriber> list = new List<Subscriber>();
            list.Add(sub);
            dictionary.Add(member.GetType(), list);
            ObservableServices.RemoveSubscriberFromDictionary(differentSub, dictionary);

            Assert.NotNull(dictionary[member2.GetType()]);

        }
    }
}
