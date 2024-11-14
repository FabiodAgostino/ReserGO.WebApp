using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Service;
using ReserGO.Utils.DTO.ExtensionMethod;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Service;

namespace ReserGO.ViewModel.ViewModel.Service
{
    public class ServiceViewModel : CompleteReserGOViewModell<DTOService, ServiceViewModel>, IServiceViewModel
    {
        private readonly IServiceService _service;

        public ServiceViewModel(IBaseServicesReserGO<ServiceViewModel> baseServices, IServiceService service) : base(baseServices)
        {
            _service = service;
            Pagination = new() { Page = 1, PageSize = 10, Filter = new() };
        }
        private GenericPagedList<DTOService> _services { get; set; }

        public GenericPagedList<DTOService> Services { get => _services; set => _services = value; }
        private GenericPagedFilter<DTOServiceFilter> _pagination { get; set; }
        public GenericPagedFilter<DTOServiceFilter> Pagination { get => _pagination; set => _pagination = value; }
        public async Task GetServices()
        {
            try
            {
                IsLoading = true;
                if (IsFirstLoad)
                    Loading("Caricamento servizi in corso...");

                var result = await _service.GetServices(Pagination);
                if(result.Success)
                {
                    Services = result.Data;
                    if (Services.TotalItemCount == 0)
                        Services = new GenericPagedList<DTOService>() { CurrentPageData = new() };
                    Pagination.TotalCount = Services.TotalItemCount;
                }
            }catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;

                if (IsFirstLoad)
                    Loading();

                IsFirstLoad = false;
                OnPropertyChanged();
            }
        }

        public async Task Insert(DTOService service)
        {
            try
            {
                IsLoading = true;
                service.ServiceName = service.ServiceName.ToUpperFirstLetter();
                var result = await _service.InsertService(service);
                if (result.Success)
                {
                    Notification("Inserimento servizio avvenuto correttamente!", NotificationColor.Success);
                    await GetServices();
                }
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        public async Task Update(DTOService service)
        {
            try
            {
                IsLoading = true;
                var result = await _service.UpdateService(service);
                if (result.Success)
                {
                    Notification("Modifica servizio avvenuto correttamente!", NotificationColor.Success);
                    await GetServices();
                }
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task Delete(DTOService service)
        {
            try
            {
                IsLoading = true;
                var result = await _service.DeleteService(service.Id);
                if (result.Success)
                {
                    Notification("Servizio eliminato correttamente", NotificationColor.Success);
                    await GetServices();
                }
                else
                {
                    Notification(result.Message, NotificationColor.Warning);
                }
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task ExecuteAction(GridAction<DTOService, DTOServiceFilter> action)
        {
            if (!String.IsNullOrEmpty(action.Error))
            {
                Notification(action.Error, NotificationColor.Error);
                return;
            }
            if (action.Items is object || action.PagingOptions is object || action.Filter is object)
            {
                switch (action.TypeActions)
                {
                    case TypeActionsGRID.PAGE_CHANGED:
                    case TypeActionsGRID.PAGE_SIZE_CHANGED:
                        Pagination.Page = action.PagingOptions.Page;
                        Pagination.PageSize = action.PagingOptions.PageSize;
                        await GetServices();
                        break;
                    case TypeActionsGRID.FILTER:
                        Pagination.Filter = action.Filter;
                        await GetServices();
                        break;
                    case TypeActionsGRID.UPDATE:
                        await Update(action.Items.FirstOrDefault());
                        break;
                    case TypeActionsGRID.INSERT:
                        await Insert(action.Items.FirstOrDefault());
                        break;
                    case TypeActionsGRID.SINGLE_DELETE:
                        await Delete(action.Items.FirstOrDefault());
                        break;

                }
            }
        }

    }
}
