using ConsoleCommandApp.Commands;
using ConsoleCommandApp.Internal;
using ConsoleCommandApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ConsoleCommandApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWeatherService, WeatherService>();
            var registrar = new ServiceCollectionRegistrar(services);

            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddCommand<HelloCommand>("SayHello")
                    .WithDescription("Say hello and get the temperature where you are!")
                    .WithAlias("hi")
                    .WithExample(new[] { "SayHello", "-z|--zip (zip code)" });
            });

            app.Run(args);
        }
    }
}