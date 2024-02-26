using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Factories
{
    public interface AbstractUserFactory
    {
        // TODO: object type should be later replaced with Member abstract class
        public object CreateTester();
        public object CreateDeveloper();
        public object CreateScrumMaster();
        public object CreateProductOwner();
    }
}
