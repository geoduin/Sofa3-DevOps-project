using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops.Domain
{
    public class Sprint
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public ISprintState? State { get; set; }
        public SprintReport? SprintReport { get; set; }
        public Pipeline? PublishingPipeline { get; set; }
        public List<BacklogItem> BacklogItems { get; set; }
        public List<Member> Members { get; set; }


        public Sprint(DateTime startDate, DateTime endDate, string name)
        {
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
            State = null;
            PublishingPipeline = null;
            BacklogItems = new List<BacklogItem>();
            Members = new List<Member>();
        }
    }
}
