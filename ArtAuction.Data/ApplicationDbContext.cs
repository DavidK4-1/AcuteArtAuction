using ArtAuction.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtAuction.Data;

public class ApplicationDbContext : IdentityDbContext {
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<UserReview> UserReviews { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}