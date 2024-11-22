using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.ExtensionMethod;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Resource.InsertResource;
using ReserGO.DTO.Extensions;
using ReserGO.DTO.ListAvailability;
using ReserGO.Service.Interface.Service;

namespace ReserGO.ViewModel.ViewModel.Resource.InsertResource
{
    public class InsertResourceViewModel : CompleteReserGOViewModell<DTOResource, InsertResourceViewModel>, IInsertResourceViewModel
    {
        private readonly IResourceService _service;
        private readonly ITranslateService t;

        public InsertResourceViewModel(IBaseServicesReserGO<InsertResourceViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            SelectedItem = new();
            Stepper = new();
            _service = service;
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(),(ObjectMessage<bool> open) => IsOpen = true);
        }
        public DTOResourceStepper Stepper { get; set; }
        public bool IsOpen { get; set; }
        public bool EnableResource { get; set; } = true;

        public async Task InsertResource()
        {
            IsLoading = true;
            Loading();
            if(Stepper.DaysSelected!=null)
            {

                SelectedItem.AvailabilityAdv = new();
                var daySelected = Stepper.DaysSelected.Select(x => x.FullName).ToList();
                var days = daySelected.GetMissingDays();
                SelectedItem.AvailabilityAdv.UnavailableByDaysOfTheWeek = new() { Data = days };

                if (Stepper.RecurringRules != null)
                {

                    var recurringRules = DTOResourceExtension.ConvertAvailabilityToUnavailability(Stepper.RecurringRules);
                    var unavailableRecurringTimeDays = new List<DTOUnavailableRecurringTimeDay>();
                    daySelected.ForEach(day =>
                    {
                        unavailableRecurringTimeDays.Add(new DTOUnavailableRecurringTimeDay(day, recurringRules));
                    });
                    SelectedItem.AvailabilityAdv.UnavailableRecurringTimeDays = unavailableRecurringTimeDays;
                }
            }
            if(!EnableResource)
            {
                if(SelectedItem.AvailabilityAdv == null)
                    SelectedItem.AvailabilityAdv = new();
                SelectedItem.AvailabilityAdv.Unavailable = true;
            }
           
            try
            {
                var result = await _service.InsertResource(SelectedItem);
                IsLoading = false;
                Loading();
                if (result.Success)
                {
                    Notification(t.Words["Risorsa inserita con successo"], NotificationColor.Success);
                    IsOpen = false;
                    Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(true), typeof(ResourceViewModel));
                }
                else
                    Notification(result.Message, NotificationColor.Warning);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }

        public async Task HandleFileSelected(IBrowserFile file)
        {
            if (file != null)
            {
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                SelectedItem.ImageData = memoryStream.ToArray();
                SelectedItem.ImageContentType = file.ContentType;
                SelectedItem.ImageName = file.Name;
            }
        }
    }
}
