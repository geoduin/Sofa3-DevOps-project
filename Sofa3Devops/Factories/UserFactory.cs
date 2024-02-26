using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Factories
{
    public class UserFactory : AbstractUserFactory
    {
        public object CreateDeveloper()
        {
            throw new NotImplementedException();
        }

        public object CreateProductOwner()
        {
            throw new NotImplementedException();
        }

        public object CreateScrumMaster()
        {
            throw new NotImplementedException();
        }

        public object CreateTester()
        {
            throw new NotImplementedException();
        }
    }
}
