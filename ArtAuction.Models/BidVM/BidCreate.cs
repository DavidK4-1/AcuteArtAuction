namespace ArtAuction.Models.BidVM;

public class BidCreate {
    public string Address { get; set; } = string.Empty;
    //[currency]
    public double Payment { get; set; }
}
