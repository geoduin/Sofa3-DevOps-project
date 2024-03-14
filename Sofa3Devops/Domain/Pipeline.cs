using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.ComponentVisitors.Visitors;
using Sofa3Devops.ComponentVisitors;

namespace Sofa3Devops.Domain
{
    public class Pipeline
    {
        public CompositeComponent BaseComposite {  get; set; }
        public List<Visitor> PipelineStages { get; set; }
        public bool SuccesFlag { get; set; }
        public bool InSession {  get; set; }

        public Pipeline(CompositeComponent stages, List<Visitor> externalVisitors) {
            this.BaseComposite = stages;
            SuccesFlag = true;
            InSession = false;
            PipelineStages = externalVisitors;
        }

        public void StartPipeline()
        {
            try
            {
                // Build 
                InSession = true;
                PipelineStages.ForEach(visitor => BaseComposite.AcceptVisitor(visitor));
                // End
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
    }
}