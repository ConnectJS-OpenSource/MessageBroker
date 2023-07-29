using BrokerService;
using MessageBroker.Core;
using MessageBroker.Core.Clients;
using MessageBroker.Core.Clients.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;


var flags = CmdFlags.Parse(args);

using var broker = new BrokerBuilder()
                .UseEndPoint(IPEndPoint.Parse(flags.BindAddress))
                .UseMemoryStore()
                .ConfigureLogger(config =>
                { 
                    config.SetMinimumLevel(Enum.Parse<LogLevel>(flags.LogType));
                    config.AddConsole();
                })
                .Build();


broker.OnClientConnected += Broker_OnClientConnected;
broker.OnClientDisconnected += Broker_OnClientDisconnected;

void Broker_OnClientConnected(object? sender, IClient e)
{
    
}

void Broker_OnClientDisconnected(object? sender, IClient e)
{
    Console.WriteLine("Client Disonnected {0}", e.Id);
}

var connectedClients = 0;

var clientStore = broker.ServiceProvider.GetRequiredService<IClientStore>();

broker.Start();

Console.WriteLine("Broker is listenning on {0}", flags.BindAddress);


while (true) { 
    
    if(clientStore.ConnectedClients.Count != connectedClients)
    {
        if(clientStore.ConnectedClients.Count > connectedClients)
        {
            var c = clientStore.ConnectedClients.Last();
            Console.WriteLine("Client Connected\n{0}-{1}", c.Id, c.ClientName);
        }
        connectedClients = clientStore.ConnectedClients.Count;
    }

    await Task.Delay(1000); 
}