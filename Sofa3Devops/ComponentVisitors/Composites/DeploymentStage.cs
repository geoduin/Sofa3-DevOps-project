using Sofa3Devops.ComponentVisitors.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class DeploymentStage : CompositeComponent
    {
        public DeploymentStage(string title) : base(title)
        {
        }

        public override bool AcceptVisitor(Visitor visitor)
        {
            return visitor.VisitDeployment(this);
        }

    }
}
