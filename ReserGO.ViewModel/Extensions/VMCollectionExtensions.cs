﻿using Microsoft.Extensions.DependencyInjection;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.Interface.FiltersComponent;
using ReserGO.ViewModel.Interface.Header;
using ReserGO.ViewModel.Interface.Historical;
using ReserGO.ViewModel.Interface.Home;
using ReserGO.ViewModel.Interface.Register;
using ReserGO.ViewModel.Interface.Resource;
using ReserGO.ViewModel.Interface.Resource.InsertResource;
using ReserGO.ViewModel.Interface.Resource.UpdateResource;
using ReserGO.ViewModel.Interface.Schedule;
using ReserGO.ViewModel.Interface.Service;
using ReserGO.ViewModel.Interface.Utils;
using ReserGO.ViewModel.ViewModel.Authentication;
using ReserGO.ViewModel.ViewModel.FiltersComponent;
using ReserGO.ViewModel.ViewModel.Header;
using ReserGO.ViewModel.ViewModel.Historical;
using ReserGO.ViewModel.ViewModel.Home;
using ReserGO.ViewModel.ViewModel.Register;
using ReserGO.ViewModel.ViewModel.Resource;
using ReserGO.ViewModel.ViewModel.Resource.InsertResource;
using ReserGO.ViewModel.ViewModel.Resource.UpdateResource;
using ReserGO.ViewModel.ViewModel.Schedule;
using ReserGO.ViewModel.ViewModel.Service;
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
            services.AddScoped<INazioneViewModel, NazioneViewModel>();
            services.AddScoped<IModalScheduleViewModel, ModalScheduleViewModel>();
            services.AddScoped<IScheduleViewModel, ScheduleViewModel>();
            services.AddScoped<IScheduleFormViewModel, ScheduleFormViewModel>();
            services.AddScoped<IDeleteScheduleViewModel, DeleteScheduleViewModel>();
            services.AddScoped<IHistoricalViewModel, HistoricalViewModel>();
            services.AddScoped<IInsertResourceViewModel, InsertResourceViewModel>();
            services.AddScoped<IDayOfWeekViewModel, DayOfWeekViewModel>();
            services.AddScoped<IServiceViewModel, ServiceViewModel>();
            services.AddScoped<IScheduleServicesViewModel, ScheduleServicesViewModel>();
            services.AddScoped<IServicesComboViewModel, ServicesComboViewModel>();
            services.AddScoped<IResourceViewModel, ResourceViewModel>();
            services.AddScoped<IUpdateResourceViewModel, UpdateResourceViewModel>();


            return services;
        }
    }
}
