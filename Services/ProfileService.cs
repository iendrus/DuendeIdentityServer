using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private UserManager<ApplicationUser> _userManager;
        public ProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Confirmation", user.EmailConfirmed.ToString()), 
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            }
            context.IssuedClaims.AddRange(claims);
        }
        
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync (context.Subject);
            context.IsActive = user != null;    
        }
   
    }
}
