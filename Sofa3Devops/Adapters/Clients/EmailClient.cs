using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Adapters.Clients
{
    public class EmailClient
    {
        public EmailClient() { }

        public void SendToMail(string sender, string recipient, string subject, string body)
        {
            Console.WriteLine($"Send by email \n From: {sender} \n To: {recipient} \n Subject: {subject} \n\n {body}");
        }
    }
}
