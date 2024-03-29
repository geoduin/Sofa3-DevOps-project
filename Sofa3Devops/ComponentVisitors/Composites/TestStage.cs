﻿using Sofa3Devops.ComponentVisitors.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Composites
{
    public class TestStage : CompositeComponent
    {
        public TestStage(string title) : base(title)
        {
        }

        public override bool AcceptVisitor(Visitor visitor)
        {
            visitor.VisitTesting(this);
            return true;
        }
    }
}
