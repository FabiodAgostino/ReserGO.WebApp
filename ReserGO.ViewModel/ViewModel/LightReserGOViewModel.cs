using Microsoft.Extensions.Logging;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface.Utils;
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
        }

        public virtual async Task Refresh()
        {

        }

        public virtual void Loading()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }
    }
}
