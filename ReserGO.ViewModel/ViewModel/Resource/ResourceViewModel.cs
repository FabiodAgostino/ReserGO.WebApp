using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Resource;
using static MudBlazor.CategoryTypes;
using System.Resources;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Miscellaneous.Message;
using ReserGO.ViewModel.ViewModel.Resource.InsertResource;
using ReserGO.ViewModel.ViewModel.Resource.UpdateResource;
using ReserGO.Service.Interface.Service;

namespace ReserGO.ViewModel.ViewModel.Resource
{
    public class ResourceViewModel : CompleteReserGOViewModell<DTOResource, ResourceViewModel>, IResourceViewModel
    {
        private readonly IResourceService _service;
        private readonly ITranslateService t;

        public ResourceViewModel(IBaseServicesReserGO<ResourceViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            _service = service;
            Pagination = new() { Page = 1, PageSize = 10, Filter = new() };
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(),async (ObjectMessage<bool> message) => await GetResourceByCompanyFiltered());
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
                    Loading(t.Words["Caricamento risorse in corso"]);

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
            DTOResource item;
            if (action.Items != null && action.Items.Count() > 0)
                item = action.Items.FirstOrDefault();
            else
                item = null;

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
                    if(item!=null)
                        Aggregator.Publish<DTOResource, ObjectMessage<DTOResource>>(new ObjectMessage<DTOResource>(item), typeof(UpdateResourceViewModel));
                    break;
                case TypeActionsGRID.SINGLE_DELETE:
                    if(item!=null)
                        await DeleteResource(item.Id.Value);
                    break;
                case TypeActionsGRID.INSERT:
                    Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(true), typeof(InsertResourceViewModel));
                    break;

            }
        }

        private async Task DeleteResource(int idResource)
        {
            try
            {
                var response = await _service.DeleteResource(idResource);
                if (response.Success)
                    Notification(response.Message, NotificationColor.Success);
                else
                    Notification(response.Message, NotificationColor.Error);

                await GetResourceByCompanyFiltered();
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }
    }
}
