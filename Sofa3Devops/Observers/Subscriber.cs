using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Observers
{
    public abstract class Subscriber
    {
        public Member NotifiedUser { get; set; }

        public Subscriber(Member NotifiedUser)
        {
            this.NotifiedUser = NotifiedUser;
        }

        public abstract void Notify(string title, string message);


    }
}
