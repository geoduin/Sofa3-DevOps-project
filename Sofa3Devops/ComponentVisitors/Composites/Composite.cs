using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public abstract class Composite : IComponent
    {
        private readonly List<IComponent> children;
        private readonly string title;

        public Composite(string title)
        {
            children = new List<IComponent>();
            this.title = title;
        }

        public bool AcceptVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public void AddComponent(IComponent component)
        {
            children.Add(component);
        }

        public bool Excecute()
        {
            throw new NotImplementedException();
        }

        public List<IComponent> GetChildren()
        {
            return children;
        }
    }
}
