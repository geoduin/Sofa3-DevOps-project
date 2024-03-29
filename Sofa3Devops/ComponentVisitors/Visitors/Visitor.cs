﻿using Sofa3Devops.ComponentVisitors.Composites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.ComponentVisitors.Visitors
{
    public interface Visitor
    {
        void VisitBuildStage(BuildStage visitor);
        void VisitAnalysis(AnalyzeStage visitor);
        void VisitTesting(TestStage visitor);
        void VisitCommand(Command command);
        void VisitDeployment(DeploymentStage stage);
        void VisitOptional(OptionalStage stage);
    }
}
