using Microsoft.Extensions.DependencyInjection;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Service.Authentication;

namespace ReserGO.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReserGOServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtAuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
