using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.ComponentVisitors.Visitors;

namespace Sofa3Devops.Domain
{
    public class Pipeline
    {
        public CompositeComponent BaseComposite {  get; set; }
        public bool SuccesFlag { get; set; }

        public Pipeline(CompositeComponent stages) {
            this.BaseComposite = stages;
            SuccesFlag = true;
        }

        public bool Action()
        {
            // Build 
            var resultBuild = BaseComposite.AcceptVisitor(new BuildVisitor());
            var resultTest = BaseComposite.AcceptVisitor(new TestVisitor());
            var resultAnalysis = BaseComposite.AcceptVisitor(new AnalyseVisitor());
            var resultDeployment = BaseComposite.AcceptVisitor(new DeploymentVisitor());
            return resultBuild;
        }

        public void StartPipeline()
        {
            try
            {
                SuccesFlag = Action();
            } catch
            {
                SuccesFlag = false;
            }
        }

        public bool HasPipelineSucceeded() {
            return SuccesFlag;
        }
    }
}