using ArtAuction.Models.BidVM;

namespace ArtAuction.Services.BidServ;

public interface IBidService {
    Task<bool> CreateBidAsync(BidCreate model);
}
