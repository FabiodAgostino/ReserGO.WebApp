namespace ReserGO.Miscellaneous.Model
{
    public class DTODayOfWeekWizard
    {
        public string FullName { get; set; }
        public string Initial => FullName.Substring(0, 1);
        public bool IsSelected { get; set; }
    }
}
