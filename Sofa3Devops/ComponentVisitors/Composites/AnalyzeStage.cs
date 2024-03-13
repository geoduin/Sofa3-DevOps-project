using Sofa3Devops.ComponentVisitors.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class AnalyzeStage : CompositeComponent
    {
        public AnalyzeStage(string title) : base(title)
        {
        }

        public override bool AcceptVisitor(Visitor visitor)
        {
            visitor.VisitAnalysis(this);
            return base.AcceptVisitor(visitor);
        }
    }
}
