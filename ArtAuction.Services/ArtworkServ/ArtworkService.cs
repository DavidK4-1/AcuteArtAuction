using Microsoft.AspNetCore.Identity;

using ArtAuction.Data;
using ArtAuction.Models.ArtworkVM;
using Microsoft.EntityFrameworkCore;
using ArtAuction.Models.GenreVM;

namespace ArtAuction.Services.ArtworkServ;

//controller will have authorize
public class ArtworkService : IArtworkService{
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<IdentityUser> _mgr;
    private readonly SignInManager<IdentityUser> _sim;
    private readonly string? _id;

    public ArtworkService(ApplicationDbContext ctx,
                          UserManager<IdentityUser> mgr,
                          SignInManager<IdentityUser> sim) {
        _ctx = ctx;
        _mgr = mgr;
        _sim = sim;

        var user = sim.Context.User;
        if (user.Identity?.Name is not null) 
            _id = mgr.GetUserId(user);
    }

    public async Task<bool?> CreateArtworkAsync(ArtworkCreate model) {
        if (_id is null)
            return null;

        Data.Entities.Artwork entity = new() {
            Description = model.Description,
            Name = model.Name,
            DateCompleted = model.CompletionDate,
            BiddingFinishDate = DateTime.Now.AddDays(5),
            UserId = _id
        };
        _ctx.Artworks.Add(entity);

        if (await _ctx.SaveChangesAsync() != 1)
            return null;

        var art = _ctx.Artworks.Entry(entity);
        foreach (var key in model.GenreKeys) {
            var genre = await _ctx.Genres.FindAsync(key);
            if (genre is not null) 
                art.Entity.Genres.Add(genre);
        }

        return await _ctx.SaveChangesAsync() == model.GenreKeys.Count;
    }

    public async Task<ArtworkDetail?> GetArtworkByIdAsync(int Id) {
        var art = await _ctx.Artworks.Include(a => a.Genres).FirstOrDefaultAsync(a => a.Id == Id);
        return art is null  
        ? null 
        : new() {
        Name = art.Name,
        Description = art.Description,
        DateCompleted = art.DateCompleted,
        BiddingFinishDate = art.BiddingFinishDate,
        Genres = art.Genres.Select(g => new GenreListItem() { // consider getting only the names, is the description really a string .Select(g => g.Name).ToList()
                    Name = g.Name,
                    Description = g.Description
               }).ToList(),
        };
    }
}
