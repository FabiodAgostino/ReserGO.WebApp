namespace ReserGO.Miscellaneous.Model
{
    public static class ItalianRegions
    {
        public const string Abruzzo = "Abruzzo";
        public const string Basilicata = "Basilicata";
        public const string Calabria = "Calabria";
        public const string Campania = "Campania";
        public const string EmiliaRomagna = "Emilia-Romagna";
        public const string FriuliVeneziaGiulia = "Friuli Venezia Giulia";
        public const string Lazio = "Lazio";
        public const string Liguria = "Liguria";
        public const string Lombardia = "Lombardia";
        public const string Marche = "Marche";
        public const string Molise = "Molise";
        public const string Piemonte = "Piemonte";
        public const string Puglia = "Puglia";
        public const string Sardegna = "Sardegna";
        public const string Sicilia = "Sicilia";
        public const string Toscana = "Toscana";
        public const string TrentinoAltoAdige = "Trentino-Alto Adige";
        public const string Umbria = "Umbria";
        public const string ValleDAosta = "Valle d'Aosta";
        public const string Veneto = "Veneto";

        public static IEnumerable<string> GetRegions()
        {
            return new List<string>
        {
            Abruzzo, Basilicata, Calabria, Campania, EmiliaRomagna,
            FriuliVeneziaGiulia, Lazio, Liguria, Lombardia, Marche,
            Molise, Piemonte, Puglia, Sardegna, Sicilia,
            Toscana, TrentinoAltoAdige, Umbria, ValleDAosta, Veneto
        };
        }
    }
}
