using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class SprintSummary
    {
        private readonly string Title;
        private readonly List<string> Notes;
        private readonly DateTime UploadDate;

        public SprintSummary(string title, List<string> notes) { 
            Title = title;
            Notes = notes;
            UploadDate = DateTime.Now;
        }
    }
}
