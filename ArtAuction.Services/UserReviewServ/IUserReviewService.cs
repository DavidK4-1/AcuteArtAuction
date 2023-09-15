using ArtAuction.Models.UserReviewVM;

namespace ArtAuction.Services.UserReviewServ;

public interface IUserReviewService {
    Task<bool?> CreateUserReviewAsync(UserReviewCreate model);
}
