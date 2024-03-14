using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public class TestVisitor : Visitor
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
        }

        public void VisitOptional(OptionalStage stage)
        {
        }

        public void VisitTesting(TestStage visitor)
        {
            Console.WriteLine("Start testing");
            foreach (var item in visitor.GetChildren())
            {
                item.AcceptVisitor(this);
            }
        }
    }
}
