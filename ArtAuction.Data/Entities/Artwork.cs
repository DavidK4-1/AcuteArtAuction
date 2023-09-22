using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtAuction.Data.Entities;

public class Artwork {
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required, MaxLength(800)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime DateCompleted { get; set; }
    [Required]
    public DateTime BiddingFinishDate { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public IdentityUser User { get; set; } = null!; 

    public virtual ICollection<Genre> Genres { get; set; } = null!;
    public Artwork() { 
        Genres = new HashSet<Genre>();
    }
}
