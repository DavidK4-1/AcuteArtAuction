using ArtAuction.Models.UserReviewVM;

namespace ArtAuction.Services.UserReviewServ;

public interface IUserReviewService {
    Task<bool> CreateUserReviewAsync(string name, UserReviewCreate model);
    Task<List<UserReviewListItem>?> GetAllUserReviewsOfAUser(string name);
}
