using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Service.Utils;
using ReserGO.Utils.DTO.Utils;
using ReserGO.Utils.Event;
using ReserGO.Utils.MVM.ViewModel;
using ReserGO.ViewModel.Interface;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel
{
    public class LightReserGOViewModel<TModel> : BaseViewModel<TModel>, ILightReserGOViewModel<TModel> where TModel : class
    {
        private readonly IJSRuntime _js;

        public LightReserGOViewModel(IEvent aggregator, ILogger logger, IJSRuntime js) : base(aggregator, logger)
        {
          InitConfigurationServer();
            _js = js;
        }
        public virtual async Task Refresh()
        {
        }
        public ConfigurationServer ConfigurationServer { get; set; }

        public void InitConfigurationServer()
        {
            var ExtendedInput = MyConfigurationAccessor.GetValue("ConfigServer:ExtendedInput");
            ConfigurationServer = new() { ExtendedInput = (ExtendedInput)Int32.Parse(ExtendedInput) };
        }

        public virtual void Loading(string text = null)
        {
            Aggregator.Publish<LoadingSpinner, ObjectMessage<LoadingSpinner>>(new ObjectMessage<LoadingSpinner>(new LoadingSpinner(IsLoading, text)), typeof(LoadingSpinnerViewModel));
        }

        private bool _isSmallView { get; set; }
        public bool IsSmallView { get => _isSmallView; set => _isSmallView = value; }

        public virtual async Task RegisterOnScreenResize(int width = 1200)
        {
            await _js.InvokeVoidAsync("onScreenResize.addResizeListener", DotNetObjectReference.Create(this), width);
        }

        [JSInvokable]
        public void OnScreenResize(bool isSmall)
        {
            IsSmallView = isSmall;
            OnPropertyChanged();
        }
    }
}
