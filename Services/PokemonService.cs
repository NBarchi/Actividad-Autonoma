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

        public async Task<PokemonResponse> GetPokemonsAsync(int offset = 0, int limit = 20)
        {
            var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit={limit}");
            var pokemons = JsonConvert.DeserializeObject<PokemonResponse>(response);

            if (pokemons?.Results != null)
            {
                foreach (var item in pokemons.Results)
                {
                    var segments = item.Url.TrimEnd('/').Split('/');
                    var id = segments.Last();
                    item.ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png";
                }
            }
            return pokemons ?? new PokemonResponse();
        }

        public async Task<Pokemon> GetPokemonAsync(string param)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{param}");
                
                // Verificamos que la respuesta no sea nula o vacía
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("La respuesta de la API es vacía.");
                }

                var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);

                if (pokemon == null)
                {
                    throw new Exception("No se encontró el Pokémon");
                }

                return pokemon;
            }
            catch (HttpRequestException httpEx)
            {
                // Error al realizar la solicitud HTTP
                throw new Exception("Error al realizar la solicitud HTTP.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                // Error al deserializar la respuesta JSON
                throw new Exception("Error al deserializar la respuesta JSON.", jsonEx);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción genérica
                throw new Exception("Ocurrió un error inesperado.", ex);
            }
        }

    }

}