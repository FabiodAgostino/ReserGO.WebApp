using Microsoft.Extensions.Logging;
using ReserGO.Miscellaneous.Message;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Utils;

namespace ReserGO.ViewModel.ViewModel.Utils
{
    public class LoadingSpinnerViewModel : LightReserGOViewModel<object>, ILoadingSpinnerViewModel
    {
        public LoadingSpinnerViewModel(IEvent aggregator, ILogger<LoadingSpinnerViewModel> logger) : base(aggregator, logger)
        {
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(), SetSpinner);
            IsLoading = false;
        }
        public void SetSpinner(ObjectMessage<bool> message)
        {
            IsLoading = message.Value;
        }
    }
}
