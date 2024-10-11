using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Utils
{
    public interface ILoadingSpinnerViewModel : ILightReserGOViewModel<object>
    {
        public LoadingSpinner LoadingSpinner { get; set; }
        public string Dimension { get; set; }
    }
}
