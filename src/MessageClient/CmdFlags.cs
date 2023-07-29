using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageClient
{
    internal class CmdFlags
    {
        [Option(longName: "broker-address", Required = true, HelpText = "Broker Address", Default = "localhost:6666")]
        public required string BrokerAddress { get; set; }

        [Option(longName: "client-id", Required = false, HelpText = "Client Id", Default = null)]
        public string? ClientId { get; set; }

        [Option(longName: "client-name", Required = false, HelpText = "Client Name", Default = "Broker-Client-Generic")]
        public string? ClientName { get; set; }

        public static CmdFlags Parse(string[] args)
        {
            return Parser.Default.ParseArguments<CmdFlags>(args).Value;
        }
    }
}
