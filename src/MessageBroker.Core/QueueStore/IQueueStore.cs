﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Core.QueueStore
{
    public interface IQueueStore
    {
        void Setup();
        void Add();
    }
}