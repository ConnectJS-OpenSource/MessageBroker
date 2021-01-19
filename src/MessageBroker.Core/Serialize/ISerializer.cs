﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker.Core.Payloads;

namespace MessageBroker.Core.Serialize
{
    public interface ISerializer
    {
        SendPayload ToSendPayload(Ack ack);
        SendPayload ToSendPayload(Nack nack);
        SendPayload ToSendPayload(Message msg);
        SendPayload ToSendPayload(Register register);
        SendPayload ToSendPayload(SubscribeQueue subscribeQueue);
        SendPayload ToSendPayload(QueueDeclare queue);
        SendPayload ToSendPayload(QueueDelete queue);

        PayloadType ParsePayloadType(Memory<byte> b);

        Ack ToAck(Memory<byte> data);
        Message ToMessage(Memory<byte> data);
        SubscribeQueue ToListenRoute(Memory<byte> data);
        Register ToSubscribe(Memory<byte> data);
        QueueDeclare ToQueueDeclareModel(Memory<byte> data);
        QueueDelete ToQueueDeleteModel(Memory<byte> data);
    }
}
