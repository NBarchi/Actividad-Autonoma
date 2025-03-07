using Newtonsoft.Json;

namespace ActividadAutonoma.Models
{
    public class Pokemon
    {
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public int PokemonHeight { get; set; }
        public int PokemonWeight { get; set; }
        public SpriteImages Images { get; set; } 
        public List<SkillInfo> Skills { get; set; }
        public List<PokemonType> PokemonTypes { get; set; }
    }

    public class SpriteImages
    {
        [JsonProperty("back_default")]
        public string DefaultBack { get; set; }

        [JsonProperty("back_female")]
        public string FemaleBack { get; set; }

        [JsonProperty("back_shiny")]
        public string ShinyBack { get; set; }

        [JsonProperty("back_shiny_female")]
        public string ShinyFemaleBack { get; set; }

        [JsonProperty("front_default")]
        public string DefaultFront { get; set; }

        [JsonProperty("front_female")]
        public string FemaleFront { get; set; }

        [JsonProperty("front_shiny")]
        public string ShinyFront { get; set; }

        [JsonProperty("front_shiny_female")]
        public string ShinyFemaleFront { get; set; }
    }

    public class SkillInfo
    {
        public Skill SkillDetail { get; set; }
    }

    public class Skill
    {
        public string SkillName { get; set; }
    }

    public class PokemonType
    {
        public TypeDetails TypeDetail { get; set; }
    }

    public class TypeDetails
    {
        public string TypeName { get; set; }
    }
}
