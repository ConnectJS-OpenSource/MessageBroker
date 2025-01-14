﻿using MessageBroker.Core.Clients;
using System;

namespace MessageBroker.Core
{
    /// <summary>
    /// Abstraction for Broker
    /// </summary>
    /// <seealso cref="Broker" />
    public interface IBroker : IDisposable
    {
        /// <summary>
        /// ServiceProvider associated with this broker, used for testing
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<IClient> OnClientConnected;
        /// <summary>
        /// 
        /// </summary>
        event EventHandler<IClient> OnClientDisconnected;

        /// <summary>
        /// Start the broker
        /// </summary>
        void Start();

        /// <summary>
        /// Stop and dispose the broker
        /// </summary>
        void Stop();
    }
}