using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.ArtworkServ;
using ArtAuction.Models.ArtworkVM;

namespace ArtAuction.Controllers;

//[Authorize] // dono how this is gonna work, uncomment later
public class ArtworkController : Controller {
    private readonly IArtworkService _service;

    public ArtworkController(IArtworkService service) {
        _service = service;
    }

    public IActionResult Index() {
        return View();
    }

    public IActionResult Create() {
        return View();
    }

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
