using ArtAuction.Data;
using ArtAuction.Data.Entities;
using ArtAuction.Models.GenreVM;

namespace ArtAuction.Services.GenreServ;

public class GenreService : IGenreService {
    private readonly ApplicationDbContext _ctx;

    public GenreService(ApplicationDbContext ctx) { 
        _ctx = ctx;
    }

    public async Task<bool?> CreateGenreAsync(GenreCreate model) {
        Genre entity = new() {
            Name = model.Name,
            Description = model.Description
        };
        _ctx.Genres.Add(entity);

        if (await _ctx.SaveChangesAsync() != 1)
            return null;

        var genre = _ctx.Genres.Entry(entity);
        foreach (var key in model.ArtworkKeys) { 
            var artwork = await _ctx.Artworks.FindAsync(key);
            if (artwork is not null)
                genre.Entity.Artworks.Add(artwork);
        }
        
        return await _ctx.SaveChangesAsync() == model.ArtworkKeys.Count;
    }
}
