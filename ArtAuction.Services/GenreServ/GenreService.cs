using Microsoft.EntityFrameworkCore;

using ArtAuction.Data;
using ArtAuction.Data.Entities;
using ArtAuction.Models.GenreVM;
using ArtAuction.Models.ArtworkVM;

namespace ArtAuction.Services.GenreServ;

public class GenreService : IGenreService {
    private readonly ApplicationDbContext _ctx;

    public GenreService(ApplicationDbContext ctx) { 
        _ctx = ctx;
    }

    public async Task<bool> CreateGenreAsync(GenreCreate model) {
        Genre entity = new() {
            Name = model.Name,
            Description = model.Description
        };
        _ctx.Genres.Add(entity);

        return await _ctx.SaveChangesAsync() == 1;
    }

    
    public async Task<List<GenreListItem>> GetAllGenresAsync()
        => await _ctx.Genres.Select(g => new GenreListItem() { 
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
        }).ToListAsync();

    public async Task<List<ArtworkListItem>?> GetArtworksFromGenresAsync(int id) {
        var genre = await _ctx.Genres.Include(g => g.Artworks).ThenInclude(g => g.User)
                                     .Where(g => g.Id == id).FirstOrDefaultAsync();

        return genre is null 
            ? null 
            : genre.Artworks.Select(a => new ArtworkListItem() {
                Name = a.Name,
                BiddingFinishDate = a.BiddingFinishDate,
                Id = a.Id,
                UserName = a.User.UserName ?? "error"
            }).ToList();
    }
}
