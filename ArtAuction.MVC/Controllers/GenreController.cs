using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.GenreServ;
using ArtAuction.Models.GenreVM;
using Microsoft.AspNetCore.Authorization;

namespace ArtAuction.Controllers;

[Authorize]
public class GenreController : Controller {
    private readonly IGenreService _service;

    public GenreController(IGenreService service) {
        _service = service;
    }

    public async Task<IActionResult> Index(int id) {
        ViewData["Genres"] = await _service.GetAllGenresAsync();
        return View(await _service.GetArtworksFromGenresAsync(id));
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreCreate model) {
        if (!ModelState.IsValid) {
            TempData["ErrorMsg"] = "Failed to create Genre";
            return View(model);
        }
        if (!await _service.CreateGenreAsync(model)) {
            TempData["ErrorMsg"] = "Failed to create Genre";
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }
}
