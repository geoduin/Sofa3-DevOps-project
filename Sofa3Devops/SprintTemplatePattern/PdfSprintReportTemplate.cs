using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintTemplatePattern
{
    public class PdfSprintReportTemplate : SprintReportTemplate
    {
        public PdfSprintReportTemplate(int version, string title, string logo) : base(version, title, logo)
        {
        }

        public override void ApplyContent(SprintReport sprintReport)
        {
            var report = sprintReport;
            Console.WriteLine("Export sprint report to pdf format.");
            // Name of sprint
            Console.WriteLine($"Name of sprint: {report.sprint.Name}");

            // Prints down all members of team.
            Console.WriteLine($"Members of the sprint:");
            report.sprint.Members.ForEach((m) => { Console.WriteLine($"Role: {m}, Name: {m.Name}"); });

            var effortPoints = report.GetEffortPointsPerDeveloper();
            var burndownChart = report.GetBurndownChart();

            // Prints down effort points per developer.
            foreach (var (member, points) in effortPoints)
            {
                string value = $"Name: {member.Name}, Effort points: {points}\n";
                Console.WriteLine(value);
                Content += value;
            }
            // Burndown chart printing
            for (int i = 0; i < burndownChart.Count; i++)
            {
                Content += $"{i}. {burndownChart[i]}\n";
            }
            Console.WriteLine("Sprint report has been exported to PDF");
        }
    }
}
