namespace ArtAuction.Models.UserReviewVM;

public class UserReviewListItem {
    public double Rating { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
}
