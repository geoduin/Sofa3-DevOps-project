using Sofa3Devops.Adapters.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Adapters
{
    public class SlackAdapter: INotification
    {

        private readonly SlackClient _client;

        public SlackAdapter(SlackClient client)
        {
            _client = client;
        }

        public void SendNotification()
        {
            throw new NotImplementedException();
        }
    }
}
