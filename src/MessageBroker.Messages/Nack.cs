﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Messages
{
    public record Nack(Guid MsgId);
}