using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Adapters
{
    public class EmailAdapter: INotificationAdapter
    {
        private readonly EmailClient _client = new EmailClient();

        public void SendNotification(string title, string message, DateTime dateOfWriting, Member recipient)
        {
            message += $"\n send at {dateOfWriting}";
            this._client.SendToMail("avansdevops@notarealemail.nl", recipient.EmailAddress, title, message);

        }
    }
}
