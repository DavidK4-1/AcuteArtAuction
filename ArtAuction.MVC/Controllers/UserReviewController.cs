using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.UserReviewServ;

namespace ArtAuction.Controllers;

public class UserReviewController : Controller {
    private readonly IUserReviewService _service;

    public UserReviewController(IUserReviewService service) {
        _service = service;
    }
}
