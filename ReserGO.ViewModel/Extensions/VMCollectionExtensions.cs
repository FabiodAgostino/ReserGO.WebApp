using Microsoft.Extensions.DependencyInjection;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.Interface.FiltersComponent;
using ReserGO.ViewModel.Interface.Header;
using ReserGO.ViewModel.Interface.Home;
using ReserGO.ViewModel.Interface.LoginHome;
using ReserGO.ViewModel.Interface.Register;
using ReserGO.ViewModel.Interface.Schedule;
using ReserGO.ViewModel.Interface.Utils;
using ReserGO.ViewModel.ViewModel.Authentication;
using ReserGO.ViewModel.ViewModel.FiltersComponent;
using ReserGO.ViewModel.ViewModel.Header;
using ReserGO.ViewModel.ViewModel.Home;
using ReserGO.ViewModel.ViewModel.LoginHome;
using ReserGO.ViewModel.ViewModel.Register;
using ReserGO.ViewModel.ViewModel.Schedule;
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
            services.AddScoped<IHeaderViewModel, HeaderViewModel>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddScoped<IComuneComboViewModel, ComuneComboViewModel>();
            services.AddScoped<ILoginHomeViewModel, LoginHomeViewModel>();
            services.AddScoped<IModalScheduleViewModel, ModalScheduleViewModel>();
            services.AddScoped<IScheduleViewModel, ScheduleViewModel>();


            return services;
        }
    }
}
