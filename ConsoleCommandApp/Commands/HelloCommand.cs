using ConsoleCommandApp.Exceptions;
using ConsoleCommandApp.Interfaces;
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
            _weatherService = weatherService;
        }

        public override async Task<int> ExecuteAsync([NotNull] CommandContext context, [NotNull] HelloCommandSettings settings)
        {
            var zipCode = PromptUserForZip(settings);
            var name = PromptUserForName(settings);

            try
            {
                var temp = await _weatherService.GetTemperatureAsync((int)zipCode);

                var tempColor = "yellow";

                if (temp < 32) tempColor = "blue";
                if (temp > 92) tempColor = "red";

                AnsiConsole.MarkupLine($"Hello {name}! The temperature for {zipCode} is a balmy [{tempColor}]{temp}[/] degrees Frankenstein.");
            }
            catch (GetTemperatureException tex)
            {
                AnsiConsole.WriteException(tex, ExceptionFormats.ShortenEverything);
            }

            return 0;
        }

        private string PromptUserForName(HelloCommandSettings settings)
        {
            string? name = settings.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = AnsiConsole.Prompt(new TextPrompt<string>("[green]What is your name?[/]")
                    .PromptStyle("white")
                    .DefaultValue("Tony")
                    .Validate(val =>
                    {
                        if (string.IsNullOrEmpty(val))
                            return ValidationResult.Error("You must enter your name!");

                        if (val.Length <= 2)
                            return ValidationResult.Error("That name is too short... No, really.");

                        return ValidationResult.Success();
                    }));
            }

            return name;
        }

        private int PromptUserForZip(HelloCommandSettings settings)
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

            return (int)zipCode;
        }
    }
}
