using ArtAuction.Models.BidVM;

namespace ArtAuction.Services.BidServ;

public interface IBidService {
    Task<bool> CreateBidAsync(int id, BidCreate model);
    Task<List<BidListItem>?> GetAllBidsForArtworkAsync(int artworkId);
}
