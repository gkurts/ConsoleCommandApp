using ConsoleCommandApp.Services;
using ConsoleCommandApp.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommandApp.Commands
{
    internal class HelloCommand : AsyncCommand<HelloCommandSettings>
    {
        private readonly IWeatherService _weatherService;

        public HelloCommand(IWeatherService weatherService)
        {
            _weatherService=weatherService;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] HelloCommandSettings settings)
        {
            int? zipCode = settings.ZipCode;

            if (zipCode == null)
            {
                zipCode = AnsiConsole.Prompt(new TextPrompt<int>("[green]What is your zip code?[/]")
                    .PromptStyle("white")
                    .DefaultValue(90210)
                    .Validate(zip =>
                    {
                        switch (zip)
                        {
                            case int z when z.ToString().Length != 5:
                                return ValidationResult.Error("Invalid zip code!");

                            default: return ValidationResult.Success();
                        }
                    }));
            }

            int temp = 88;
            try
            {
                temp = await _weatherService.GetTemperatureAsync((int)zipCode);

                var tempColor = "yellow";

                if (temp < 32) tempColor = "blue";
                if (temp > 92) tempColor = "red";

                AnsiConsole.MarkupLine($"Hello there! The temperature for {zipCode} is a balmy [{tempColor}]{temp}[/] degrees Frankenstein.");
            }
            catch (GetTemperatureException tex)
            {
                AnsiConsole.WriteException(tex, ExceptionFormats.ShortenEverything);
            }

            return 0;
        }
    }
}
