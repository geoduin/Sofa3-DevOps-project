using Sofa3Devops.BacklogStates;
using System.Diagnostics;

namespace Sofa3Devops.Domain
{
    public class SprintReport
    {
        public string CompanyName { get; set; } = "Company";
        public string ProjectName { get; set; } = "";
        public List<int> BurndownChart {  get; set; }
        public Dictionary<Member, int> EffortPointsPerDeveloper { get;}
        public readonly Sprint sprint;

        public SprintReport(Sprint sprint)
        {
            this.sprint = sprint;
            BurndownChart = new List<int>();
            EffortPointsPerDeveloper = new Dictionary<Member, int>();
        }

        public Dictionary<Member, int> GetEffortPointsPerDeveloper()
        {
            foreach (var item in sprint.BacklogItems)
            {
                GetEffortPointsOfItem(item);
                foreach (var activity in item.Activities)
                {
                    GetEffortPointsOfItem(activity);
                }
            }
            return EffortPointsPerDeveloper;
        }

        public List<int> GetBurndownChart()
        {
            int allPoints = sprint.BacklogItems.Sum((x) => x.EffortPoints + x.Activities.Sum(x => x.EffortPoints));
            BurndownChart.Add(allPoints);

            for (int i = 1; i < sprint.BacklogItems.Count; i++)
            {
                int newValue = BurndownChart[i - 1];
                if (sprint.BacklogItems[i].State.GetType() == typeof(FinishedState))
                {
                    newValue = BurndownChart[i - 1] - sprint.BacklogItems[i].EffortPoints;
                }
                BurndownChart.Add(newValue);
            }
            return BurndownChart;
        }
    
        private void GetEffortPointsOfItem(BacklogItem item)
        {
            var underdeveloper = item.ResponsibleMember;
            if (underdeveloper != null)
            {
                var p = item.EffortPoints;
                var cp = EffortPointsPerDeveloper.GetValueOrDefault(underdeveloper, 0);
                cp += p;
                EffortPointsPerDeveloper[underdeveloper] = cp;
            }
        }
    }
}