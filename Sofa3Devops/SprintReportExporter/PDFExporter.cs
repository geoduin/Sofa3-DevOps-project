using Sofa3Devops.Domain;
using Sofa3Devops.SprintTemplatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintReportExporter
{
    public class PDFExporter : ISprintExportStrategy
    {
        private readonly SprintReportTemplate sprintTemplatePattern;

        public PDFExporter(SprintReportTemplate sprintTemplatePattern) {
            this.sprintTemplatePattern = sprintTemplatePattern;
        }

        public string ExportReport(Sprint sprint)
        {
            var report = new SprintReport(sprint);
            Console.WriteLine("Export sprint report to pdf format.");
            var result = sprintTemplatePattern.BuildReport(report);

            Console.WriteLine("Export sprint report to pdf format has completed.");
            return "PDF";
        }
    }
}
