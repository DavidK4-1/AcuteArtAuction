namespace ArtAuction.Models.BidVM;

public class BidCreate {
    public string Address { get; set; } = string.Empty;
    public int ArtworkId { get; set; }
    //[currency]
    public double Payment { get; set; }
}
