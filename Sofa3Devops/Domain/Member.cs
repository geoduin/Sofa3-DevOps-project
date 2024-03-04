﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        protected Member(string emailAddress, string slackUserName)
        {
            PostedThreads = new List<CommentThread>();
            PostedResponses = new List<Response>();
            EmailAddress = emailAddress;
            SlackUserName = slackUserName;
        }

        public Member(string name) {
            Name = name;
            PostedResponses = new List<Response>();
            PostedThreads = new List<CommentThread>();
        }
    }
}
