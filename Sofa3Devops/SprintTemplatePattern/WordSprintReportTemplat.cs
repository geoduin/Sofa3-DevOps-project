using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintTemplatePattern
{
    public class WordSprintReportTemplate : SprintReportTemplate
    {
        public WordSprintReportTemplate(int version, string title, string logo) : base(version, title, logo)
        {
        }

        public override void ApplyContent(SprintReport sprintReport)
        {
            Console.WriteLine("Start formatting word document");
            Sprint sprint = sprintReport.sprint;

            // Prints down all members of team.
            Console.WriteLine($"Members of the sprint:");
            sprint.Members.ForEach((m) => { 
                Console.WriteLine($"<p>Role: {m}, Name: {m.Name}</p>"); 
            });

            var effortPoints = sprintReport.GetEffortPointsPerDeveloper();
            var burndownChart = sprintReport.GetBurndownChart();

            // Prints down effort points per developer.
            foreach (var (member, points) in effortPoints)
            {
                string value = $"<p> Name: {member.Name}, Effort points: {points}</p>";
                Console.WriteLine(value);
                Content += value;
            }
            // Burndown chart printing
            for (int i = 0; i < burndownChart.Count; i++)
            {
                Content += $"<p>{i}. {burndownChart[i]} </p>";
            }
        }
    }
}
