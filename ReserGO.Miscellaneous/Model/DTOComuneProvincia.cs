namespace ReserGO.Miscellaneous.Model
{
    public class Provincia
    {
        public string Nome { get; set; }
        public string Regione { get; set; }
    }

    public class DTOComuneProvincia
    {
        public string Nome { get; set; }
        public Provincia Provincia { get; set; }
    }
}
