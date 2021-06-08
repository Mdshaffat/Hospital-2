using HospitalAPI.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HospitalAPI.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> FindByUserByClaimsPrincipleWithAddressAsync(this UserManager<ApplicationUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<ApplicationUser> FindByEmailFromClaimsPrinciple(this UserManager<ApplicationUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
