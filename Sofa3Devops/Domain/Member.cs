using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public abstract class Member
    {
        public string Name { get; set; }
        public List<CommentThread> PostedThreads { get; set; }
        public List<Response> PostedResponses { get; set; }
        public string EmailAddress { get; set; }
        public string SlackUserName { get; set; }


        public Member(string name, string emailAddress, string slackUserName) {
            Name = name;
            PostedResponses = new List<Response>();
            PostedThreads = new List<CommentThread>();
            EmailAddress = emailAddress;
            SlackUserName = slackUserName;
        }
    }
}
