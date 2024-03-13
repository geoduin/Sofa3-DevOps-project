using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class Command : IComponent
    {
        private readonly string title;
        private readonly string command;
        private bool SuccessFlag { get; set; } = true;
        private Visitor? Visitor { get; set; }

        public Command(string title, string command) {
            this.title = title;
            this.command = command;
        }

        public bool AcceptVisitor(Visitor visitor)
        {
            try
            {
                visitor.VisitCommand(this);
                // Perhaps some logic

                return SuccessFlag;
            }
            catch
            {
                return false;
            }
        }
    }
}
