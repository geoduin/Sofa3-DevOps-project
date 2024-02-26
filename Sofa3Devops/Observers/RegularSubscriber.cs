using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Observers
{
    public class RegularSubscriber : Subscriber
    {
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Notify()
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
