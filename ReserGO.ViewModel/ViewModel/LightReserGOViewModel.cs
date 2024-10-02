using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface.Utils;
using ReserGO.Service.Service.Utils;
using ReserGO.Utils.DTO.Utils;
using ReserGO.Utils.Event;
using ReserGO.Utils.MVM.ViewModel;
using ReserGO.ViewModel.Interface;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel
{
    public class LightReserGOViewModel<TModel> : BaseViewModel<TModel>, ILightReserGOViewModel<TModel> where TModel : class
    {
        public LightReserGOViewModel(IEvent aggregator, ILogger logger) : base(aggregator, logger)
        {
          InitConfigurationServer();
        }
        public virtual async Task Refresh()
        {
        }
        public ConfigurationServer ConfigurationServer { get; set; }

        public void InitConfigurationServer()
        {
            var ExtendedInput = MyConfigurationAccessor.GetValue("ConfigServer:ExtendedInput");
            ConfigurationServer = new() { ExtendedInput = (ExtendedInput)Int32.Parse(ExtendedInput) };
        }

        public virtual void Loading()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }
    }
}
