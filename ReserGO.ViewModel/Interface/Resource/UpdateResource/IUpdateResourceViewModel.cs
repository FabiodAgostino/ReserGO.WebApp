﻿using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Resource.UpdateResource
{
    public interface IUpdateResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public bool IsOpen { get; set; }
        Task HandleFileSelected(IBrowserFile file);
    }
}
