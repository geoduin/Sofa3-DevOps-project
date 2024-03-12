using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public interface Visitor
    {
        bool VisitBuildStage(AnalyzeStage visitor);
        bool VisitAnalysis(AnalyzeStage visitor);
        bool VisitTesting(AnalyzeStage visitor);
    }
}
