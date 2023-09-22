using Microsoft.AspNetCore.Identity;

using ArtAuction.Data;
using ArtAuction.Models.UserReviewVM;
using ArtAuction.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

    public async Task<bool> CreateUserReviewAsync(string name, UserReviewCreate model) {
        var user = await _mgr.FindByNameAsync(name);
        if (user is null)
            return false;

        UserReview entity = new() {
            UserName = (await _ctx.Users.FindAsync(_id))?.UserName ?? "error",
            Title = model.Title,
            Contents = model.Contents,
            Rating = model.Rating,
            UserId = user.Id
        };

        _ctx.UserReviews.Add(entity);
        return await _ctx.SaveChangesAsync() == 1;
    }

    public async Task<List<UserReviewListItem>?> GetAllUserReviewsOfAUser(string name) {
        var user = await _mgr.FindByNameAsync(name);
        if (user is null)
            return null;

        return await _ctx.UserReviews.Where(r => r.UserId == user.Id).Select(r => new UserReviewListItem() {
            UserName = r.UserName,
            Title = r.Title,
            Contents = r.Contents,
            Rating = r.Rating
        }).ToListAsync();
        }
}
