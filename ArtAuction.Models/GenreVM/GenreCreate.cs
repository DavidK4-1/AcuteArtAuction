namespace ArtAuction.Models.GenreVM;

public class GenreCreate {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<int> ArtworkKeys { get; set; } = null!;
}
