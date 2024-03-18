using Sofa3Devops.Domain;
using Sofa3Devops.SprintTemplatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintReportExporter
{
    public class WordExporter : ISprintExportStrategy
    {
        private readonly SprintReportTemplate sprintReportTemplate;

        public WordExporter(SprintReportTemplate sprintReportTemplate) {
            this.sprintReportTemplate = sprintReportTemplate;
        }

        public string ExportReport(Sprint sprint)
        {
            Console.WriteLine("Export sprint report to Microsoft word format.");
            var report = new SprintReport(sprint);
            sprintReportTemplate.BuildReport(report);
            Console.WriteLine("Export sprint report to Microsoft word format done.");
            return "Microsoft word";
        }
    }
}
