namespace ReserGO.Miscellaneous.Model
{
    public class LoadingSpinner
    {
        public bool IsLoading { get; set; }
        public string? Text { get; set; }
        public bool NoTranslate { get; set; }
        public LoadingSpinner(bool isLoading)
        {
            IsLoading = isLoading;
        }
        public LoadingSpinner(bool isLoading, string text)
        {
            IsLoading = isLoading;
            Text = text;
        }

        public LoadingSpinner(bool isLoading, string text, bool noTranslate)
        {
            IsLoading = isLoading;
            Text = text;
            NoTranslate = noTranslate;
        }
    }
}
