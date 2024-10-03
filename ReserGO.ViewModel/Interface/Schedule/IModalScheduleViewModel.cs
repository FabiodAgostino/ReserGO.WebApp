using Microsoft.AspNetCore.Components;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IModalScheduleViewModel : ICompleteReserGOViewModel<GenericModal<int>>
    {
        public bool IsOpen { get; set; }
        void Close();
    }
}
