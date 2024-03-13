using Sofa3Devops.ComponentVisitors.Composites;

namespace Sofa3Devops.Domain
{
    public class Pipeline
    {
        public CompositeComponent stages {  get; set; }
        public bool SuccesFlag { get; set; }

        public Pipeline(CompositeComponent stages) {
            this.stages = stages;
            SuccesFlag = false;
        }

        public void StartPipeline()
        {
            Console.WriteLine("Start pipeline");
            SuccesFlag = stages.Excecute();
        }

        public bool HasPipelineSucceeded() {
            return SuccesFlag;
        }
    }
}