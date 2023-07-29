// creating a new broker client
using MessageBroker.Client;
using MessageBroker.Client.ConnectionManagement;
using MessageClient;
using System.Net;
using System.Text;

var flags = CmdFlags.Parse(args);

await using var brokerClient = new BrokerClientFactory().GetClient();

Console.WriteLine("Client-Id - {0}", flags.ClientId);
// connecting the client to server
brokerClient.Connect(new ClientConnectionConfiguration
{
    EndPoint = new DnsEndPoint(flags.BrokerAddress.Split(':')[0], int.Parse(flags.BrokerAddress.Split(':')[1])),
    AutoReconnect = true,
    ClientId = flags.ClientId,
    ClientName = flags.ClientName
});

await brokerClient.DeclareTopicAsync(flags.ClientId, flags.ClientId);
var subscription = await brokerClient.GetTopicSubscriptionAsync(flags.ClientId);

subscription.MessageReceived += (msg) =>
{
    var data = Encoding.ASCII.GetString(msg.Data.ToArray());
    Console.WriteLine("Message Recieved\n{0}", data);
    msg.Ack();
};

while (true)
{
    Console.WriteLine("Connected {0}", brokerClient.Connected);
    await brokerClient.PublishAsync(flags.ClientId, Encoding.ASCII.GetBytes(DateTime.Now.ToString()));
    await Task.Delay(1000);
}