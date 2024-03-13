using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class CompositeComponent : IComponent
    {
        private readonly List<IComponent> children;
        private readonly string title;
        private Visitor Visitor {  get; set; }

        public CompositeComponent(string title)
        {
            children = new List<IComponent>();
            this.title = title;
        }

        public virtual bool AcceptVisitor(Visitor visitor)
        {
            Visitor = visitor;
            children.ForEach((child) => {
                child.AcceptVisitor(visitor);
            });
            return true;
        }

        public void AddComponent(IComponent component)
        {
            children.Add(component);
        }

        public bool Excecute()
        {
            try
            {
                Console.WriteLine(this.title);
                children.ForEach(c => c.Excecute());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<IComponent> GetChildren()
        {
            return children;
        }
    }
}
