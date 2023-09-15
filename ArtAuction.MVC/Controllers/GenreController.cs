using Microsoft.AspNetCore.Mvc;

using ArtAuction.Services.GenreServ;

namespace ArtAuction.Controllers;

public class GenreController : Controller {
    private readonly IGenreService _service;

    public GenreController(IGenreService service) {
        _service = service;
    }
}
