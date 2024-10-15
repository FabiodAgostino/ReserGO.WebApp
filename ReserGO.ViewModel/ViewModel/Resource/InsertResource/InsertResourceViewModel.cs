using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface;
using ReserGO.ViewModel.Interface.Resource.InsertResource;

namespace ReserGO.ViewModel.ViewModel.Resource.InsertResource
{
    public class InsertResourceViewModel : CompleteReserGOViewModell<DTOResource, InsertResourceViewModel>, IInsertResourceViewModel
    {
        public InsertResourceViewModel(IBaseServicesReserGO<InsertResourceViewModel> baseServices) : base(baseServices)
        {
            SelectedItem = new();
        }
        public int SelectedIndex { get; set; }
        public async Task InsertResource()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }
    }
}
