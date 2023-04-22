using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace ConsoleCommandApp.Internal
{
    /// <summary>
    /// For using Microsoft.Extensions.DependencyInjection in our console app.
    /// Logic from: https://github.com/nils-org/Spectre.Console.Registrars.Microsoft-DI
    /// </summary>
    public sealed class ServiceCollectionRegistrar : ITypeRegistrar
    {
        private readonly IServiceCollection builder;

        public ServiceCollectionRegistrar(IServiceCollection builder)
        {
            this.builder = builder;
        }

        public ITypeResolver Build()
        {
            return new TypeResolver(builder.BuildServiceProvider());
        }

        public void Register(Type service, Type implementation)
        {
            builder.AddSingleton(service, implementation);
        }

        public void RegisterInstance(Type service, object implementation)
        {
            builder.AddSingleton(service, implementation);
        }

        public void RegisterLazy(Type service, Func<object> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            builder.AddSingleton(service, _ => func());
        }

        private sealed class TypeResolver : ITypeResolver
        {
            private readonly IServiceProvider provider;

            public TypeResolver(IServiceProvider provider)
            {
                this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            }

            public object? Resolve(Type? type)
            {
                if (type == null)
                {
                    return null;
                }

                return provider.GetService(type);
            }
        }
    }
}
