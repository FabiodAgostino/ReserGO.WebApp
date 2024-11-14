using ReserGO.Service.Interface.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class ImageService : IImageService
    {
        public string GetImage(byte[]? blob) =>  blob.Count()>0 ? $"data:image/png;base64,{Convert.ToBase64String(blob)}" : String.Empty;
    }
}
