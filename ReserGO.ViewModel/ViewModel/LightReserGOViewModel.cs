using Microsoft.Extensions.Logging;
using ReserGO.Utils.Event;
using ReserGO.Utils.MVM.ViewModel;
using ReserGO.ViewModel.Interface;

namespace ReserGO.ViewModel.ViewModel
{
    public class LightReserGOViewModel<TModel> : BaseViewModel<TModel>, ILightReserGOViewModel<TModel> where TModel : class
    {
        public LightReserGOViewModel(IEvent aggregator, ILogger logger) : base(aggregator, logger)
        {
        }
    }
}
