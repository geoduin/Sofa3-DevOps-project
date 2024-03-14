using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public class DeploymentVisitor : Visitor
    {
        public void VisitAnalysis(AnalyzeStage visitor)
        {
        }

        public void VisitBuildStage(BuildStage visitor)
        {
        }

        public void VisitCommand(Command command)
        {
            Console.WriteLine(command.command);
        }

        public void VisitDeployment(DeploymentStage stage)
        {
            foreach (var item in stage.GetChildren())
            {
                item.AcceptVisitor(this);
            }
        }

        public void VisitOptional(OptionalStage stage)
        {
        }

        public void VisitTesting(TestStage visitor)
        {
        }
    }
}
