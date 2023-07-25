using MessageBroker.Core;
using MessageBroker.Core.Clients.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;


var SocketPort = 5556;

using var broker = new BrokerBuilder()
                .UseEndPoint(IPEndPoint.Parse($"0.0.0.0:{SocketPort}"))
                .UseMemoryStore()
                .ConfigureLogger(config =>
                {
                    config.SetMinimumLevel(LogLevel.Debug);
                    config.AddConsole();
                })
                .Build();

var clientStore = broker.ServiceProvider.GetRequiredService<IClientStore>();

broker.Start();

while (true) { 
    
    await Task.Delay(1000); 
}