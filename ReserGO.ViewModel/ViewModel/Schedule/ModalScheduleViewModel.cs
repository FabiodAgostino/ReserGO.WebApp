using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ModalScheduleViewModel : CompleteReserGOViewModell<GenericModal<int>, ModalScheduleViewModel>, IModalScheduleViewModel
    {
        public ModalScheduleViewModel(IBaseServicesReserGO<ModalScheduleViewModel> baseServices) : base(baseServices)
        {
            Aggregator.Subscribe<ObjectMessage<GenericModal<int>>>(GetType(), OpenModal);
        }
        public bool IsOpen { get; set; }

        public async void OpenModal(ObjectMessage<GenericModal<int>> message)
        {
            IsOpen = true;
            OnPropertyChanged();
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
