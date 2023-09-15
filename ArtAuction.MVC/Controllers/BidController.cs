using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ArtAuction.Models.BidVM;
using ArtAuction.Services.BidServ;

namespace ArtAuction.Controllers;

//[Authorize] // dono how this is gonna work, uncomment later
public class BidController : Controller {
    private readonly IBidService _service;

    public BidController(IBidService service) {
        _service = service;
    }

    public IActionResult Index() {
        return View();
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BidCreate model) {
        if (!await _service.CreateBidAsync(model))
            TempData["ErrorMsg"] = "cannot set bid on artwork";
        return View(model); 
    }
}
