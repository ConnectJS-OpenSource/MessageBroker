using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BrokerService
{
    public class CmdFlags
    {
        [Option(longName: "bind-address", Required = false, HelpText = "Port Address", Default = "0.0.0.0:6666")]
        public string BindAddress { get; set; }

        [Option(longName: "log-level", Required = false, HelpText = "Trace, Debug, Warning, Information, Error, Critical, None", Default = "None")]
        public string LogType { get; set; }

        public static CmdFlags Parse(string[] args)
        {
            return Parser.Default.ParseArguments<CmdFlags>(args).Value;
        }
    }
}
