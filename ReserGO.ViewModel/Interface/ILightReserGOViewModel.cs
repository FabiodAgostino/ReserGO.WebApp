using ReserGO.Utils.DTO.Utils;
using ReserGO.Utils.MVM.Interface;

namespace ReserGO.ViewModel.Interface
{
    public interface ILightReserGOViewModel<TModel> : IBaseViewModel<TModel>
    {
        Task Refresh();
        ConfigurationServer ConfigurationServer { get; set; }
    }
}
