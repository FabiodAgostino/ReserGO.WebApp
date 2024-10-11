namespace ReserGO.Miscellaneous.Model
{
    public class LoadingSpinner
    {
        public bool IsLoading { get; set; }
        public string? Text { get; set; }

        public LoadingSpinner(bool isLoading)
        {
            IsLoading = isLoading;
        }
        public LoadingSpinner(bool isLoading, string text)
        {
            IsLoading = isLoading;
            Text = text;
        }
    }
}
