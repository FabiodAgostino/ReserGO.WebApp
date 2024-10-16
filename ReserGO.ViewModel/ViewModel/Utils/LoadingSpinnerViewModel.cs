using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Utils;

namespace ReserGO.ViewModel.ViewModel.Utils
{
    public class LoadingSpinnerViewModel : LightReserGOViewModel<object>, ILoadingSpinnerViewModel
    {
        public LoadingSpinnerViewModel(IEvent aggregator, ILogger<LoadingSpinnerViewModel> logger, IJSRuntime js): base(aggregator, logger, js)
        {
            Aggregator.Subscribe<ObjectMessage<LoadingSpinner>>(GetType(), SetSpinner);
        }
        public LoadingSpinner LoadingSpinner { get; set; }
        private string _dimension {  get; set; }    
        public string Dimension
        {
            get
            {
                if (LoadingSpinner == null || String.IsNullOrEmpty(LoadingSpinner.Text))
                    return "250px !important;\n";
                else
                    return "350px !important;\n";
            }
            set
            {
                _dimension = value;
            }
        }
        public void SetSpinner(ObjectMessage<LoadingSpinner> message)
        {
            LoadingSpinner = message.Value;
            OnPropertyChanged();
        }
    }
}
