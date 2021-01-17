﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Core.Models
{
    /// <summary>
    /// the message contains data that is sent by publisher and received by subscriber
    /// </summary>
    public ref struct Message
    {
        public Guid Id { get; init; }
        public string Route { get; init; }
        public Memory<byte> Data { get; init; }
        public byte[] OriginalMessageData { get; init; }

    }
}