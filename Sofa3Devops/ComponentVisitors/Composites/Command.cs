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
        private bool SuccessFlag { get; set; } = false;

        public Command(string title, string command) {
            this.title = title;
            this.command = command;
        }

        public bool AcceptVisitor(Visitor visitor)
        {
            throw new NotImplementedException();
        }

        public bool Excecute()
        {
            throw new NotImplementedException();
        }
    }
}
