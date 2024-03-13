using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public class BuildVisitor : Visitor
    {
        public bool VisitAnalysis(AnalyzeStage visitor)
        {
            return true;
        }

        public bool VisitBuildStage(BuildStage visitor)
        {
            Console.WriteLine("========");
            Console.WriteLine("Visit Build stage");
            var commands = visitor.GetChildren();

            foreach (var command in commands)
            {
                Console.WriteLine(command.ToString());
            }
            return true;
        }

        public void VisitCommand(Command command)
        {
        }

        public bool VisitTesting(TestStage visitor)
        {
            return true;
        }
    }
}
