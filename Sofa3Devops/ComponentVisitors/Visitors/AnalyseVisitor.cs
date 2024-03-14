using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public class AnalyseVisitor: Visitor
    {
        public bool VisitAnalysis(AnalyzeStage visitor)
        {
            Console.WriteLine("Visit code analysis stage.");
            foreach(var el in visitor.GetChildren())
            {
                el.AcceptVisitor(this);
            }

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
            return true;
        }

        public bool VisitTesting(TestStage visitor)
        {
            return true;
        }
    }
}
