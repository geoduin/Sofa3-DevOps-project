using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintReportExporter
{
    public class PDFExporter : ISprintExportStrategy
    {
        public string ExportReport(Sprint sprint)
        {
            var report = new SprintReport(sprint);
            Console.WriteLine("Export sprint report to pdf format.");
            // Name of sprint
            Console.WriteLine($"Name of sprint: {sprint.Name}");

            // Prints down all members of team.
            Console.WriteLine($"Members of the sprint:");
            sprint.Members.ForEach((m)=> { Console.WriteLine($"Role: {m}, Name: {m.Name}");});

            var effortPoints = report.GetEffortPointsPerDeveloper();
            var burndownChart = report.GetBurndownChart();

            // Prints down effort points per developer.
            foreach (var (member, points) in effortPoints)
            {
                string value = $"Name: {member.Name}, Effort points: {points}";
                Console.WriteLine(value);
            }
            // Burndown chart
            burndownChart.ForEach(x => Console.WriteLine($"{x}"));
            
            return "PDF";
        }
    }
}
