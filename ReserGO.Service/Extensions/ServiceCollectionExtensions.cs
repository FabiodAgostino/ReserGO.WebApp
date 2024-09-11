using Microsoft.Extensions.DependencyInjection;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Service.Authentication;
using ReserGO.Service.Service.Home;

namespace ReserGO.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReserGOServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtAuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHomeService, HomeService>();

            return services;
        }
    }
}
