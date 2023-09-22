using ArtAuction.Models.ArtworkVM;
using ArtAuction.Models.GenreVM;

namespace ArtAuction.Services.GenreServ;

public interface IGenreService {
    Task<bool> CreateGenreAsync(GenreCreate model);
    Task<List<GenreListItem>> GetAllGenresAsync();
    Task<List<ArtworkListItem>?> GetArtworksFromGenresAsync(int id);
}
