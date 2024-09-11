using Microsoft.Extensions.DependencyInjection;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.Interface.Home;
using ReserGO.ViewModel.Interface.Utils;
using ReserGO.ViewModel.ViewModel.Authentication;
using ReserGO.ViewModel.ViewModel.Home;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.Extensions
{
    public static class VMCollectionExtensions
    {
        public static IServiceCollection AddReserGOViewModels(this IServiceCollection services)
        {
            services.AddScoped<IHomeViewModel, HomeViewModel>();
            services.AddScoped<ILoginViewModel, LoginViewModel>();
            services.AddScoped<ILoadingSpinnerViewModel, LoadingSpinnerViewModel>();

            return services;
        }
    }
}
