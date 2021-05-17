﻿namespace Tests.Clients.ConnectionManagement
{
    public class ConnectionManagerTests
    {
        // [Fact]
        // public void Connect_ConnectionFailedForFirstTimeAndSucceedsTheSecondTime_ConnectionIsEstablished()
        // {
        //     // create mocks 
        //     var receiveDataProcessor = new Mock<IReceiveDataProcessor>();
        //     var binaryDataProcessor = new Mock<IBinaryDataProcessor>();
        //     var tcpSocket = new Mock<ITcpSocket>();
        //     
        //     // setup mocks
        //     tcpSocket.SetupSequence(s => s.Connect(It.IsAny<IPEndPoint>())).Throws<Exception>();
        //     tcpSocket.SetupSequence(s => s.Connect(It.IsAny<IPEndPoint>())).Pass();
        //     
        //     
        //     // setup UUT
        //     var connectionManager = new ConnectionManager(receiveDataProcessor.Object, binaryDataProcessor.Object);
        //     
        //     connectionManager.SetAlternativeTcpSocketForTesting(tcpSocket.Object);
        //     connectionManager.Connect(It.IsAny<IPEndPoint>());
        //     
        //     // verify connection manager is in connected state
        //     Assert.True(connectionManager.Connected);
        //     Assert.NotNull(connectionManager.ClientSession);
        // }
        //
        //
        // [Fact]
        // public void Disconnect_SocketIsClosed()
        // {
        //     var clientSession = new Mock<IClientSession>();
        //     var receiveDataProcessor = new Mock<IReceiveDataProcessor>();
        //     var binaryDataProcessor = new Mock<IBinaryDataProcessor>();
        //     var tcpSocket = new Mock<ITcpSocket>();
        //     
        //     
        //     // setup UUT
        //     var connectionManager = new ConnectionManager(receiveDataProcessor.Object, binaryDataProcessor.Object);
        //     
        //     connectionManager.SetAlternativeTcpSocketForTesting(tcpSocket.Object);
        //     connectionManager.Connect(new IPEndPoint(0, 0));
        //     connectionManager.Disconnect();
        //     
        //     tcpSocket.Verify(t => t.Disconnect(It.IsAny<bool>()));
        //     clientSession.Verify(cs => cs.Close());
        // }
    }
}