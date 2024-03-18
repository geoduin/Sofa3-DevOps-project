using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintReportExporter
{
    public class WordExporter : ISprintExportStrategy
    {
        public string ExportReport(Sprint sprint)
        {
            Console.WriteLine("Export sprint report to Microsoft word format.");
            return "Microsoft word";
        }
    }
}
