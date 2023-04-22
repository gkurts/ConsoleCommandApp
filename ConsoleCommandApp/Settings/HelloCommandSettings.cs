using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandApp.Settings
{
    public class HelloCommandSettings : CommandSettings
    {
        [Description("What is your zip code?")]
        [CommandOption("-z|--zip")]
        public int? ZipCode { get; init; }
    }
}
