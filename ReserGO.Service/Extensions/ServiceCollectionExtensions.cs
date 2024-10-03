using Microsoft.Extensions.DependencyInjection;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Utils;
using ReserGO.Service.Service;
using ReserGO.Service.Service.Authentication;
using ReserGO.Service.Service.Utils;

namespace ReserGO.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReserGOServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseServicesReserGO<>), typeof(BaseServicesReserGO<>));
            services.AddScoped<IJwtAuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped(typeof(IBaseServicesReserGO<>), typeof(BaseServicesReserGO<>));


            return services;
        }
    }
}
