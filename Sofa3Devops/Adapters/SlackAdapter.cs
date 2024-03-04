﻿using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Adapters
{
    public class SlackAdapter: INotification
    {

        private readonly SlackClient _client = new SlackClient();

        public void SendNotification(string title, string message, DateTime dateOfWriting, List<Member> recipients)
        {
            message += $"\n Send at: {dateOfWriting}";
            foreach (var recipient in recipients)
            {
                this._client.Sent("@someslackusername", recipient.SlackUserName, title, message);

            }
        }
    }
}
