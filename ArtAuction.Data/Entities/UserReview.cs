using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtAuction.Data.Entities;

public class UserReview {
    [Key]
    public int Id { get; set; }
    [Required]
    public int Rating { get; set; }
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required, MaxLength(800)]
    public string Contents { get; set; } = string.Empty;

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = Guid.NewGuid().ToString();
    public IdentityUser User { get; set; } = null!; 
}
