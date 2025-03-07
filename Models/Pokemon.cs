using Newtonsoft.Json;


namespace ActividadAutonoma.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Sprites Sprites { get; set; } 
        public List<TypeInfo> Types { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }
    public class TypeDetail
    {
        public string Name { get; set; }
    }

    public class TypeInfo
    {
        public TypeDetail Type { get; set; }
    } 

    public class PokemonResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<PokemonItem> Results { get; set; }
    }

    public class PokemonItem
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}