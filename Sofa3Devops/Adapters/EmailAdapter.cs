using Sofa3Devops.Adapters.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Adapters
{
    public class EmailAdapter: INotification
    {
        private readonly EmailClient _client;

        public EmailAdapter(EmailClient client)
        {
            _client = client;
        }

        public void SendNotification()
        {
            throw new NotImplementedException();
        }
    }
}
