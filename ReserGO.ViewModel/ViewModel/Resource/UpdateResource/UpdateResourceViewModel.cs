﻿using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Resource.UpdateResource;

namespace ReserGO.ViewModel.ViewModel.Resource.UpdateResource
{
    public class UpdateResourceViewModel : CompleteReserGOViewModell<DTOResource, UpdateResourceViewModel>, IUpdateResourceViewModel
    {
        private readonly IResourceService _service;

        public UpdateResourceViewModel(IBaseServicesReserGO<UpdateResourceViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            Aggregator.Subscribe(GetType(), (ObjectMessage<DTOResource> message) => OpenModal(message));
            _service = service;
        }

        private bool _oldEnableResource;
        public bool EnableResource
        {
            get
            {
                if(SelectedItem.AvailabilityAdv!=null)
                {
                    return !SelectedItem.AvailabilityAdv.Unavailable;
                }
                return true;
            }
            set
            {
                if (SelectedItem.AvailabilityAdv == null)
                    SelectedItem.AvailabilityAdv = new();

                SelectedItem.AvailabilityAdv.Unavailable = !value;

                if(!RulesChanged.Contains(AvailabilityType.UnavailableGeneral.ToString()))
                    RulesChanged.Add(AvailabilityType.UnavailableGeneral.ToString());
            }
        }

        public async Task GetFullResource(int idResource)
        {
            try
            {
                IsLoading = true;
                Loading();
                var result = await _service.GetFullResource(idResource, null);
                if (result.Success)
                {
                    SelectedItem = result.Data;
                    _oldEnableResource = SelectedItem.AvailabilityAdv != null && SelectedItem.AvailabilityAdv.Unavailable ? SelectedItem.AvailabilityAdv.Unavailable : true;
                    IsOpen = true;
                }
                else
                    Notification(result.Message, NotificationColor.Warning);
                OnPropertyChanged();

            }catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                Loading();
            }
        }
        private async void OpenModal(ObjectMessage<DTOResource> message)
        {
            if(message.Value!=null && message.Value.Id.HasValue)
            {
                await GetFullResource(message.Value.Id.Value);
            }
        }
        public bool IsOpen { get; set; }
        public List<string> RulesChanged { get; set; } = new();

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

        public async Task UpdateResource()
        {
            try
            {
                SelectedItem.ResourcesAvailability = null;
                if(SelectedItem.AvailabilityAdv!=null)
                    SelectedItem.AvailabilityAdv.RulesChanged = RulesChanged;
                var r = await _service.UpdateResource(SelectedItem);
            }catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }
    }
}
