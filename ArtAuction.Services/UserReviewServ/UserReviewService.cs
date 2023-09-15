using Microsoft.AspNetCore.Identity;

using ArtAuction.Data;
using ArtAuction.Models.UserReviewVM;
using ArtAuction.Data.Entities;

namespace ArtAuction.Services.UserReviewServ;

public class UserReviewService : IUserReviewService {
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<IdentityUser> _mgr;
    private readonly SignInManager<IdentityUser> _sim;
    private readonly string? _id;

    public UserReviewService(ApplicationDbContext ctx,
                            UserManager<IdentityUser> mgr,
                            SignInManager<IdentityUser> sim) {
        _ctx = ctx;
        _mgr = mgr;
        _sim = sim;

        var user = sim.Context.User;
        if (user.Identity?.Name is not null)
            _id = mgr.GetUserId(user);
    }

    public async Task<bool?> CreateUserReviewAsync(UserReviewCreate model) {
        UserReview entity = new() {
            UserName = (await _ctx.Users.FindAsync(_id))?.UserName ?? "error",
            Title = model.Title,
            Contents = model.Contents,
            Rating = model.Rating,
            UserId = model.UserId
        };

        _ctx.UserReviews.Add(entity);
        return await _ctx.SaveChangesAsync() == 1;
    }
}
