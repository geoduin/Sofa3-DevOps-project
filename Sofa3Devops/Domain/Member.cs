using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public List<CommentThread> PostedThreads { get; set; }
        public List<Response> PostedResponses { get; set; }
    }
}
