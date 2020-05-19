using Microsoft.Extensions.DependencyInjection;
using RegistrationLibrary.Interfaces;

namespace RegistrationLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegistrationLibraryDefaults(this IServiceCollection services)
        {
            services.AddSingleton<IRegistrator, Registrator>();
            return services;
        }
    }
}