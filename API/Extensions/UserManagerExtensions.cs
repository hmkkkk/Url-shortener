using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user, bool withLinks = false)
        {
            var username = user.FindFirstValue(ClaimTypes.GivenName);

            return withLinks 
                ? await userManager.Users.SingleOrDefaultAsync(x => x.UserName == username)
                : await userManager.Users.Include(x => x.UserLinks).SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}