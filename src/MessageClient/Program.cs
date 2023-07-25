// creating a new broker client
using MessageBroker.Client;
using MessageBroker.Client.ConnectionManagement;
using System.Net;
using System.Text;

var SocketPort = 5556;

await using var brokerClient = new BrokerClientFactory().GetClient();

// connecting the client to server
brokerClient.Connect(new ClientConnectionConfiguration
{
    EndPoint = new DnsEndPoint("localhost", SocketPort),
    AutoReconnect = true,
});

await brokerClient.DeclareTopicAsync("Client-A", "91d9e255-0706-48cd-ad12-920afab66205");
var subscription = await brokerClient.GetTopicSubscriptionAsync("Client-A");

subscription.MessageReceived += (msg) =>
{
    var data = Encoding.ASCII.GetString(msg.Data.ToArray());
    Console.WriteLine("Message Recieved - {0}", data);
    msg.Ack();
};

while (true)
{
    await brokerClient.PublishAsync("91d9e255-0706-48cd-ad12-920afab66205", Encoding.ASCII.GetBytes(DateTime.Now.ToString()));
    await Task.Delay(1000);
}