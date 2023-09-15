using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtAuction.Data.Entities;

public class Bid {
    [Key]
    public int Id { get; set; }
    [Required]
    public double Payment { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(50)]
    public string Address { get; set; } = string.Empty;
    [Required]
    public DateTime DatePlaced { get; set; }

    [ForeignKey(nameof(Artwork))]
    public int ArtworkId { get; set; }
    public Artwork Artwork { get; set; } = null!;
}
