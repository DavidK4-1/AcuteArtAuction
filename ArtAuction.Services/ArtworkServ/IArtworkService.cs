using ArtAuction.Models.ArtworkVM;
using ArtAuction.Models.GenreVM;

namespace ArtAuction.Services.ArtworkServ;

public interface IArtworkService {
    Task<bool?> CreateArtworkAsync(ArtworkCreate model);
    Task<List<ArtworkListItem>?> GetAllArtworkRelatedToUserAsync();
    Task<List<GenreListItem>> GetFullGenreSelectionAsync();
    Task<List<ArtworkListItem>?> GetAllArtworkAsync();
    Task<ArtworkDetail?> GetArtworkByIdAsync(int Id);
}
