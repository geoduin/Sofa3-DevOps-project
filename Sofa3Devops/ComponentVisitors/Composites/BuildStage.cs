using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class BuildStage : CompositeComponent
    {
        public BuildStage(string title) : base(title)
        {
        }

        public override bool AcceptVisitor(Visitor visitor)
        {
            visitor.VisitBuildStage(this);
            return base.AcceptVisitor(visitor);
        }
    }
}
