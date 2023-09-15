namespace ArtAuction.Models.UserReviewVM;

public class UserReviewCreate {
    public string Title { get; set; } = string.Empty;
    public string Contents { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public int Rating { get; set; }
}