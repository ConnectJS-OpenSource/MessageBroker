using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBroker.Common.Models
{
    public struct ClientInfo
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
