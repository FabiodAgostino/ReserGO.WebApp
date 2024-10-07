using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ModalScheduleViewModel : CompleteReserGOViewModell<DTOResource, ModalScheduleViewModel>, IModalScheduleViewModel
    {
        private readonly IScheduleService _service;
        public ModalScheduleViewModel(IBaseServicesReserGO<ModalScheduleViewModel> baseServices, IScheduleService service) : base(baseServices)
        {
            Aggregator.Subscribe<ObjectMessage<GenericModal<int>>>(GetType(), OpenModal);
            _service = service;
        }
        public bool IsOpen { get; set; }

        public async void OpenModal(ObjectMessage<GenericModal<int>> message)
        {
            IsOpen = true;
            OnPropertyChanged();
        }

        public async Task GetFullResource(int idResource)
        {
            try
            {
                await _service.GetFullResource(idResource);
            }catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
