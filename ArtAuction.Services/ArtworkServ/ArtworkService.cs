using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ArtAuction.Data;
using ArtAuction.Models.ArtworkVM;
using ArtAuction.Models.GenreVM;
using ArtAuction.Data.Entities;

namespace ArtAuction.Services.ArtworkServ;

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

        Artwork entity = new() {
            Description = model.Description,
            Name = model.Name,
            DateCompleted = model.CompletionDate,
            //have to change it by hand, but whatever for now
            BiddingFinishDate = DateTime.Now.AddDays(1),
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

    public async Task<List<ArtworkListItem>?> GetAllArtworkRelatedToUserAsync()
        => await _ctx.Artworks.Include(a => a.User).Where(a => a.UserId == _id).Select(a => new ArtworkListItem() { 
            Id = a.Id,
            Name = a.Name,
            UserName = a.User.UserName ?? "error",
            BiddingFinishDate = a.BiddingFinishDate
        }).ToListAsync();

    public async Task<List<ArtworkListItem>?> GetAllArtworkAsync()
        => await _ctx.Artworks.Include(a => a.User).Select(a => new ArtworkListItem(){
            Id = a.Id,
            Name = a.Name,
            UserName = a.User.UserName ?? "error",
            BiddingFinishDate = a.BiddingFinishDate
        }).ToListAsync();

    public async Task<List<GenreListItem>> GetFullGenreSelectionAsync()
        => await _ctx.Genres.Select(g => new GenreListItem() {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
        }).ToListAsync();

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
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
               }).ToList(),
        };
    }
}
