using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MessageBroker.Core.Clients.Store
{
    /// <inheritdoc />
    public class ClientStore : IClientStore
    {
        private readonly ConcurrentDictionary<Guid, IClient> _sendQueues;

        /// <summary>
        /// Instantiates a new <see cref="ClientStore" />
        /// </summary>
        public ClientStore()
        {
            _sendQueues = new ConcurrentDictionary<Guid, IClient>();
        }

        List<IClient> IClientStore.ConnectedClients => _sendQueues.Values.ToList();


        /// <inheritdoc />
        public void Add(IClient client)
        {
            _sendQueues[client.Id] = client;
        }

        /// <inheritdoc />
        public void Remove(IClient client)
        {
            _sendQueues.TryRemove(client.Id, out var _);
        }

        /// <inheritdoc />
        public int TotalClientsConnected()
        {
            return _sendQueues.Count;
        }

        /// <inheritdoc />
        public bool TryGet(Guid clientId, out IClient client)
        {
            return _sendQueues.TryGetValue(clientId, out client);
        }
    }
}