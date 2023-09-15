using System.ComponentModel.DataAnnotations;

namespace ArtAuction.Data.Entities;

public class Genre {
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    public ICollection<Artwork> Artworks { get; set; } = null!;
    public Genre() { 
        Artworks = new HashSet<Artwork>();
    }
}