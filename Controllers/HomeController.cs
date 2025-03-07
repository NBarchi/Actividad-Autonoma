using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ActividadAutonoma.Models;
using ActividadAutonoma.Services;
using System.Threading.Tasks;

namespace ActividadAutonoma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PokemonService _pokemonService;

        public HomeController(ILogger<HomeController> logger, PokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        public async Task<IActionResult> Index(int offset = 0)
        {
            var pokemonListResponse = await _pokemonService.GetPokemonsAsync(offset, 20);

            ViewBag.Offset = offset;
            ViewBag.Next = offset + 20;
            ViewBag.Previous = (offset - 20) >= 0 ? offset - 20 : (int?)null;

            // Se ha actualizado para usar PokemonItems en lugar de Results
            return View(pokemonListResponse.PokemonItems);
        }

        public async Task<IActionResult> Details(string name)
        {
            var pokemonDetails = await _pokemonService.GetPokemonAsync(name);
            return View(pokemonDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
