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
        public bool VisitAnalysis(AnalyzeStage visitor)
        {
            return true;
        }

        public bool VisitBuildStage(BuildStage visitor)
        {
            return true;
        }

        public void VisitCommand(Command command)
        {
            Console.WriteLine(command.command);
        }

        public bool VisitDeployment(DeploymentStage stage)
        {
            foreach (var item in stage.GetChildren())
            {
                item.AcceptVisitor(this);
            }
            return true;
        }

        public bool VisitTesting(TestStage visitor)
        {
            return true;
        }
    }
}
