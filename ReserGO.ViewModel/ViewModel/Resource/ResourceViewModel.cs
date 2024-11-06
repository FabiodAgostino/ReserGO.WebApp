using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Resource;
using static MudBlazor.CategoryTypes;
using System.Resources;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Resource
{
    public class ResourceViewModel : CompleteReserGOViewModell<DTOResource, ResourceViewModel>, IResourceViewModel
    {
        private readonly IResourceService _service;

        public ResourceViewModel(IBaseServicesReserGO<ResourceViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            _service = service;
            Pagination = new() { Page = 1, PageSize = 10, Filter = new() };
        }

        private GenericPagedList<DTOResource> _resources { get; set; }

        public GenericPagedList<DTOResource> Resources { get => _resources; set => _resources = value; }
        private GenericPagedFilter<DTOResourceFilter> _pagination { get; set; }
        public GenericPagedFilter<DTOResourceFilter> Pagination { get => _pagination; set => _pagination = value; }

        public async Task GetResourceByCompanyFiltered()
        {
            try
            {
                IsLoading = true;
                if (IsFirstLoad)
                    Loading("Caricamento risorse in corso...");

                var result = await _service.GetResourceByCompanyFiltered(Pagination);
                if (result.Success)
                {
                    Resources = result.Data;
                    if (Resources.TotalItemCount == 0)
                        Resources = new GenericPagedList<DTOResource>() { CurrentPageData = new() };
                    Pagination.TotalCount = Resources.TotalItemCount;
                }
            }
            catch (Exception ex)
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

        public async Task ExecuteAction(GridAction<DTOResource, DTOResourceFilter> action)
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
                        await GetResourceByCompanyFiltered();
                        break;
                    case TypeActionsGRID.FILTER:
                        Pagination.Filter = action.Filter;
                        await GetResourceByCompanyFiltered();
                        break;
                    case TypeActionsGRID.UPDATE:
                        break;
                    case TypeActionsGRID.INSERT:
                        break;
                    case TypeActionsGRID.SINGLE_DELETE:
                        break;

                }
            }
        }
    }
}
