using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.ArtworkServ;
using ArtAuction.Models.ArtworkVM;

namespace ArtAuction.Controllers;

public class ArtworkController : Controller {
    private readonly IArtworkService _service;

    public ArtworkController(IArtworkService service) {
        _service = service;
    }

    [Authorize]
    public async Task<IActionResult> Index() {
        return View(await _service.GetAllArtworkRelatedToUserAsync());
    }

    public async Task<IActionResult> All() {
        return View(await _service.GetAllArtworkAsync());
    }

    public async Task<IActionResult> Detail(int id) {
        return View(await _service.GetArtworkByIdAsync(id));
    }

    [Authorize]
    public async Task<IActionResult> Create() {
        ViewData["Genres"] = await _service.GetFullGenreSelectionAsync();
        return View();
    }

    [Authorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArtworkCreate model) {
        switch (await _service.CreateArtworkAsync(model)) {
            case null:
                TempData["ErrorMsg"] = "object failed to create"; return View(model);
            case false:
                TempData["ErrorMsg"] = "not all genres were added"; return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}
