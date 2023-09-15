using ArtAuction.Models.GenreVM;

namespace ArtAuction.Services.GenreServ;

public interface IGenreService {
    Task<bool?> CreateGenreAsync(GenreCreate model);
}
