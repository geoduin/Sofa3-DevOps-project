using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.SprintTemplatePattern
{
    public abstract class SprintReportTemplate
    {
        protected string Content { get; set; }
        public int Version { get; set; }
        public string Base64Logo { get; set; }
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }

        public SprintReportTemplate(int version, string title, string logo)
        {
            Title = title;
            Version = version;
            Content = "";
            DateOfCreation = DateTime.Now;
            Base64Logo = logo;
        }
        protected virtual void ApplyHeader()
        {
            Content += $"{Base64Logo}\n";
        }

        protected virtual void ApplyFooter()
        {
            Content += $"{Base64Logo} - Version {Version} - {DateOfCreation.Year}\n";
        }

        public abstract void ApplyContent(SprintReport sprintReport);

        public string BuildReport(SprintReport sprintReport)
        {
            ApplyHeader();
            ApplyContent(sprintReport);
            ApplyFooter();
            return Content;
        }
    }
}
