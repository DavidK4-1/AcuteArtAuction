using ArtAuction.Models.ArtworkVM;

namespace ArtAuction.Services.ArtworkServ;

public interface IArtworkService {
    Task<bool?> CreateArtworkAsync(ArtworkCreate model);
}
