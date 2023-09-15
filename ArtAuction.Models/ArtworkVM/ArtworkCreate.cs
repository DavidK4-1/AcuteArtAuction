namespace ArtAuction.Models.ArtworkVM;

public class ArtworkCreate {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CompletionDate { get; set; }
    public ICollection<int> GenreKeys { get; set; } = null!;
}
