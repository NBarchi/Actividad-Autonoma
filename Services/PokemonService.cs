using ActividadAutonoma.Models;
using Newtonsoft.Json;

namespace ActividadAutonoma.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonListResponse> GetPokemonsAsync(int offset = 0, int limit = 20)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit={limit}");
            var pokemonsResponse = JsonConvert.DeserializeObject<PokemonListResponse>(response);

            if (pokemonsResponse?.PokemonItems != null)
            {
                foreach (var item in pokemonsResponse.PokemonItems)
                {
                    // Extraemos el ID desde la URL
                    var segments = item.Url.TrimEnd('/').Split('/');
                    var id = segments.Last();

                    // Generamos la URL de la imagen
                    item.PokemonImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png";
                }
            }

            return pokemonsResponse ?? new PokemonListResponse();
        }

        public async Task<Pokemon> GetPokemonAsync(string param)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{param}");
            var pokemonDetails = JsonConvert.DeserializeObject<Pokemon>(response);

            if (pokemonDetails == null)
            {
                throw new Exception("No se pudo deserializar el Pokémon.");
            }

            return pokemonDetails;
        }
    }

    public class PokemonListResponse
    {
        public int TotalCount { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        public List<PokemonSummary> PokemonItems { get; set; }
    }

    public class PokemonSummary
    {
        public string PokemonName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PokemonImageUrl { get; set; } = string.Empty;
    }
}
