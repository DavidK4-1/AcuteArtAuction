using Microsoft.AspNetCore.Identity;

using ArtAuction.Data;
using ArtAuction.Models.BidVM;
using ArtAuction.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtAuction.Services.BidServ;

public class BidService : IBidService {
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<IdentityUser> _mgr;
    private readonly SignInManager<IdentityUser> _sim;
    private readonly string? _id;

    public BidService (ApplicationDbContext ctx,
                       UserManager<IdentityUser> mgr,
                       SignInManager<IdentityUser> sim) {
        _ctx = ctx;
        _mgr = mgr;
        _sim = sim;

        var user = sim.Context.User;
        if (user.Identity?.Name is not null) 
            _id = mgr.GetUserId(user);
    }

    public async Task<bool> CreateBidAsync(int id, BidCreate model) {
        // might need a null check 
        if ((await _ctx.Artworks.FindAsync(id))?.BiddingFinishDate < DateTime.Now || _id is null)
            return false;

        Bid entity = new() { 
            Name = (await _ctx.Users.FindAsync(_id))?.UserName??"error",
            Address = model.Address,
            ArtworkId = id,
            DatePlaced = DateTime.Now,
            Payment = model.Payment
        };

        _ctx.Bids.Add(entity);
        return await _ctx.SaveChangesAsync() == 1;
    }

    public async Task<List<BidListItem>?> GetAllBidsForArtworkAsync(int artworkId)
        => await _ctx.Bids.Where(b => b.ArtworkId == artworkId).Select(b => new BidListItem() {
            ArtworkId = b.ArtworkId,
            Name = b.Name,
            DatePlaced = b.DatePlaced,
            Payment = b.Payment
        }).ToListAsync();
}
