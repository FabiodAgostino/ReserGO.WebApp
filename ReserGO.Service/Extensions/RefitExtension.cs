using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Service.Interface.Utils;
using ReserGO.Service.Service.Authentication;

namespace ReserGO.Service.Extensions
{
    public static class RefitExtensions
    {
        public static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
        {
            var serverapi = configuration["serverapi"];

            services.AddTransient<ApiMessageHandler>();
            services.AddScoped(provider =>
            {
                var handler = provider.GetRequiredService<ApiMessageHandler>();
                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri($"{serverapi}api/Utils")
                };
                return RestService.For<IHomeService>(httpClient);
            });
            services.AddScoped(provider =>
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri($"{serverapi}api/Auth")
                };
                return RestService.For<ILoginService>(httpClient);
            });

            services.AddScoped(provider =>
            {
                var handler = provider.GetRequiredService<ApiMessageHandler>();
                var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri($"{serverapi}api/Resource")
                };
                return RestService.For<IScheduleService>(httpClient);
            });

            services.AddScoped(provider =>
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://axqvoqvbfjpaamphztgd.functions.supabase.co")
                };
                return RestService.For<IComuneService>(httpClient);
            });

            return services;
        }
    }
}
