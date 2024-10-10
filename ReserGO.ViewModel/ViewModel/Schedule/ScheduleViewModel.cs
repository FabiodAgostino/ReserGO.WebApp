using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ScheduleViewModel : CompleteReserGOViewModell<DTOResource, ScheduleViewModel>, IScheduleViewModel
    {
        private readonly IResourceService _service;

        public ScheduleViewModel(IBaseServicesReserGO<ScheduleViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            _service = service;
        }
        public async Task OpenModalCalendar()
        {
            var modal = new GenericModal<DTOResource>("", SelectedItem);
            Aggregator.Publish<GenericModal<DTOResource>, ObjectMessage<GenericModal<DTOResource>>>(new ObjectMessage<GenericModal<DTOResource>>(modal), typeof(ModalScheduleViewModel));
        }

        public bool LoadingFullResource { get; set; } = false;

        public override async Task Refresh()
        {
            try
            {
                IsLoading = true;
                var res = await _service.GetResourcesByCompany();
                if(res.Success)
                    List = res.Data;
                else
                    Notification(res.Message, NotificationColor.Warning);
            }catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading=false;
            }
        }

        public async Task GetFullResource(int IdResource)
        {
            try
            {
                LoadingFullResource = true;
                var res = await _service.GetFullResource(IdResource, null);
                if (res.Success)
                    SelectedItem = res.Data;
                else
                    Notification(res.Message, NotificationColor.Warning);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                LoadingFullResource = false;
                OnPropertyChanged();
            }
        }
    }
}
