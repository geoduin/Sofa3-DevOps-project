using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sofa3Devops.Adapters.Clients
{
    public class SlackClient
    {

        public SlackClient() { }
        public void Sent(string sender, string recipient, string subject, string body)
        {
            Console.WriteLine($"Send by slack \n From: {sender} \n To: {recipient} \n Subject: {subject} \n\n {body}");

        }
    }
}
