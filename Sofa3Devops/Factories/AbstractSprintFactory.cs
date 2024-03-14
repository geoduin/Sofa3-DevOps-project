using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Factories
{
    // This will contain the methods of concrete classes related to the sprints:
    // - Sprints
    // - Pipelines related, because they are related to the sprints.
    public interface AbstractSprintFactory
    {
        public Sprint CreateSprint(DateTime start, DateTime end, string name);
    }

    public class DevelopmentSprintFactory: AbstractSprintFactory
    {
        public Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            return new DevelopmentSprint(start, end, name);
        }
    }

    public class ReleaseSprintFactory : AbstractSprintFactory
    {
        public Sprint CreateSprint(DateTime start, DateTime end, string name)
        {
            var composites = CreateCompositeComponent();
            var pipeline = CreatePipeline(composites);
            return new ReleaseSprint(start, end, name, pipeline);
        }

        public Pipeline CreatePipeline(CompositeComponent compositeComponent)
        {
            return new Pipeline(compositeComponent);
        }

        public CompositeComponent CreateCompositeComponent() {
            return new CompositeComponent("CI/CD Pipeline");
        }
    }
}
