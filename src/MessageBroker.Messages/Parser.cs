﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Messages
{
    public class Parser
    {
        private static byte[] _delimiter;
        public static Span<byte> Delimiter
        {
            get
            {
                if (_delimiter != null)
                {
                    return _delimiter.AsSpan();
                }

                var delimiter = "\n";
                var delimiterB = Encoding.ASCII.GetBytes(delimiter);
                _delimiter = delimiterB;
                return delimiterB;
            }
        }
        public object Parse(Span<byte> payload)
        {
            var indexOfMessageType = payload.IndexOf(Delimiter);
            var messasgeType = payload.Slice(0, indexOfMessageType);
            var messageBody = payload.Slice(indexOfMessageType + Delimiter.Length);

            var messageTypeStr = Encoding.ASCII.GetString(messasgeType);

            return messageTypeStr switch
            {
                MessageTypes.Ack => ToAck(messageBody),
                MessageTypes.Nack => ToNack(messageBody),
                MessageTypes.Message => ToMessage(messageBody),
                MessageTypes.ListenRoute => ToListenRoute(messageBody),
                MessageTypes.UnlistenListenRoute => ToListenRoute(messageBody),
                MessageTypes.RegisterPublisher => ToRegister(messageBody),
                MessageTypes.RegisterSubscriber => ToRegister(messageBody),
                MessageTypes.UnRegister => ToRegister(messageBody),
                _ => null
            };
        }

        private Ack ToAck(Span<byte> data)
        {
            var dataTrimmed = data.TrimEnd(Delimiter);
            var messageId = new Guid(dataTrimmed);
            return new Ack(messageId);
        }

        private Nack ToNack(Span<byte> data)
        {
            var dataTrimmed = data.TrimEnd(Delimiter);
            var messageId = new Guid(dataTrimmed);
            return new Nack(messageId);
        }

        private Message ToMessage(Span<byte> data)
        {
            var messaageIdRes = data.FindNext(0, Delimiter);
            var routeRes = data.FindNext(messaageIdRes.Index, Delimiter);
            var dataRes = data.FindNext(routeRes.Index, Delimiter);

            var messageId = new Guid(messaageIdRes.Result);
            var route = Encoding.UTF8.GetString(routeRes.Result);
            var rentedMemory = ArrayPool<byte>.Shared.Rent(dataRes.Result.Length);
            dataRes.Result.CopyTo(rentedMemory);

            return new Message(messageId, route, rentedMemory.AsMemory().Trim(Encoding.UTF8.GetBytes("\0")));
        }

        private Listen ToListenRoute(Span<byte> data)
        {
            var routeRes = data.FindNext(0, Delimiter);
            var route = Encoding.UTF8.GetString(routeRes.Result);

            return new Listen(route);
        }

        private Register ToRegister(Span<byte> data)
        {
            return new Register();
        }

        private IEnumerable<Memory<byte>> Split(Memory<byte> b)
        {
            var start = 0;
            while (true)
            {
                var index = b.Span.IndexOf(Delimiter);
                if (index > 0 && index != b.Length)
                {
                    yield return b.Slice(start, index);
                }
                else
                {
                    yield break;
                }
                start = index + Delimiter.Length;
            }
        }
    }

    public static class SpanExtension
    {
        public static NextSpan FindNext(this Span<byte> b, int fromIndex, ReadOnlySpan<byte> symbol)
        {
            var s = b.Slice(fromIndex).TrimStart(symbol);
            var index = s.IndexOf(symbol);

            return new NextSpan(s.Slice(0, index), index + fromIndex + symbol.Length);
        }
    }

    public ref struct NextSpan
    {
        public Span<byte> Result { get; }
        public int Index { get; }

        public NextSpan(Span<byte> result, int index) : this()
        {
            Result = result;
            Index = index;
        }

    }
}