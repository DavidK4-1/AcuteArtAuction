using Microsoft.AspNetCore.Identity;

using ArtAuction.Data;
using ArtAuction.Models.BidVM;
using ArtAuction.Data.Entities;

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

    public async Task<bool> CreateBidAsync(BidCreate model) {
        // might need a null check 
        if ((await _ctx.Artworks.FindAsync(model.ArtworkId))?.BiddingFinishDate < DateTime.Now || _id is null)
            return false;

        Bid entity = new() { 
            Name = (await _ctx.Users.FindAsync(_id))?.UserName??"error",
            Address = model.Address,
            ArtworkId = model.ArtworkId,
            DatePlaced = DateTime.Now,
            Payment = model.Payment
        };

        _ctx.Bids.Add(entity);
        return await _ctx.SaveChangesAsync() == 1;
    }
}
