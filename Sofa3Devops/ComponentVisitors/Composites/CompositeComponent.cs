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

        public CompositeComponent(string title)
        {
            children = new List<IComponent>();
            this.title = title;
        }

        public virtual bool AcceptVisitor(Visitor visitor)
        {
            try
            {
                foreach (var child in children)
                {                    
                    var r = child.AcceptVisitor(visitor);
                }
                return true;
            }
            catch
            {
                throw new InvalidOperationException($"command: {title} has failed.");
            }
            
        }

        public void AddComponent(IComponent component)
        {
            children.Add(component);
        }


        public List<IComponent> GetChildren()
        {
            return children;
        }
    }
}
