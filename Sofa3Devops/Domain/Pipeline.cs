using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.ComponentVisitors.Visitors;
using Sofa3Devops.ComponentVisitors;

namespace Sofa3Devops.Domain
{
    public class Pipeline
    {
        public CompositeComponent BaseComposite {  get; set; }
        public bool SuccesFlag { get; set; }
        public bool InSession {  get; set; }

        public Pipeline(CompositeComponent stages) {
            this.BaseComposite = stages;
            SuccesFlag = true;
            InSession = false;
        }

        public void StartPipeline()
        {
            try
            {
                // Build 
                InSession = true;

                BaseComposite.AcceptVisitor(new BuildVisitor());
                BaseComposite.AcceptVisitor(new TestVisitor());
                BaseComposite.AcceptVisitor(new AnalyseVisitor());
                BaseComposite.AcceptVisitor(new DeploymentVisitor());
                BaseComposite.AcceptVisitor(new OptionalVisitor());
                // End o
                InSession = false;
                SuccesFlag = true;
            } catch
            {
                SuccesFlag = false;
            }
        }

        public bool HasPipelineSucceeded() {
            return SuccesFlag;
        }

        public void ExtendPipeline(CompositeComponent component)
        {
            BaseComposite.AddComponent(component);
        }
    }
}