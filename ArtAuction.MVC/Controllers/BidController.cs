using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ArtAuction.Models.BidVM;
using ArtAuction.Services.BidServ;

namespace ArtAuction.Controllers;

[Authorize] // dono how this is gonna work, uncomment later
public class BidController : Controller {
    private readonly IBidService _service;

    public BidController(IBidService service) {
        _service = service;
    }

    public async Task<IActionResult> Index(int id) {
        return View(await _service.GetAllBidsForArtworkAsync(id));
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int id, BidCreate model) {
        if (!ModelState.IsValid) {
            TempData["ErrorMsg"] = "cannot set bid on artwork";
            return View(model);
        }
        if (!await _service.CreateBidAsync(id, model)) {
            TempData["ErrorMsg"] = "cannot set bid on artwork";
            return View(model);
        }
        return RedirectToAction("All", "Artwork");
    }
}
