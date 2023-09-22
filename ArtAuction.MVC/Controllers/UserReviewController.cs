using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.UserReviewServ;
using ArtAuction.Models.UserReviewVM;
using Microsoft.AspNetCore.Authorization;

namespace ArtAuction.Controllers;

[Authorize]
public class UserReviewController : Controller {
    private readonly IUserReviewService _service;

    public UserReviewController(IUserReviewService service) {
        _service = service;
    }

    public async Task<IActionResult> Index(string id) {
        return View(await _service.GetAllUserReviewsOfAUser(id)); 
    }

    public IActionResult Create(string id) {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string id, UserReviewCreate model) {// throws error after creation :(
        if (!await _service.CreateUserReviewAsync(id, model)) {
            TempData["ErrorMsg"] = "Failed to create Genre";
            return View(model);
        }
        return RedirectToAction("All", "Artwork");
    } 
}
