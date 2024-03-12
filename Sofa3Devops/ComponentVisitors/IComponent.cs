using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.ComponentVisitors
{
    public interface IComponent
    {
        bool AcceptVisitor(Visitor visitor);
        bool Excecute();
    }
}
