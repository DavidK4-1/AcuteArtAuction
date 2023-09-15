using ArtAuction.Models.GenreVM;

namespace ArtAuction.Models.ArtworkVM;

public class ArtworkDetail {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateCompleted { get; set; }
    public DateTime BiddingFinishDate { get; set; }
    public ICollection<GenreListItem> Genres { get; set; } = null!;
}