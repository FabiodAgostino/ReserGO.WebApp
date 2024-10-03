using System.Text.Json.Serialization;

public class ComboNazione
{
    [JsonPropertyName("name")]
    public NameInfo Name { get; set; }

    [JsonPropertyName("translations")]
    public Translations Translations { get; set; }
}

public class NameInfo
{
    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }
}

public class Translations
{
    [JsonPropertyName("ita")]
    public LanguageInfo Italian { get; set; }

    // Aggiungi altre lingue se necessario
}

public class LanguageInfo
{
    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }
}

public class DTONazione
{
    public string? Name { get; set; }

    public DTONazione(ComboNazione combo)
    {
        Name = combo?.Translations?.Italian?.Common ?? combo?.Name?.Common;
    }
}
