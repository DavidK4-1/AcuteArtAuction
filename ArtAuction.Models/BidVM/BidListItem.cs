namespace ArtAuction.Models.BidVM;

public class BidListItem {
    public int ArtworkId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Payment { get; set; }
    public DateTime DatePlaced { get; set; }
}
