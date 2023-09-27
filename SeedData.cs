using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            //context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Role, "Staff1"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere"),
                            new Claim(JwtClaimTypes.Role, "Staff2"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("bob created");
            }
            else
            {
                Log.Debug("bob already exists");
            }

            var admin = userMgr.FindByNameAsync("admin").Result;
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(admin, "Admin!0").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "admin"),
                            new Claim(JwtClaimTypes.GivenName, "admin"),
                            new Claim(JwtClaimTypes.Role, "Admin"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("admin created");
            }
            else
            {
                Log.Debug("admin already exists");
            }

            var alf = userMgr.FindByNameAsync("alf").Result;
            if (alf == null)
            {
                alf = new ApplicationUser
                {
                    UserName = "alf",
                    Email = "alf@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(alf, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alf, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alf Tanner"),
                            new Claim(JwtClaimTypes.GivenName, "Alf"),
                            new Claim(JwtClaimTypes.FamilyName, "Tanner"),
                            new Claim(JwtClaimTypes.WebSite, "http://alf.com"),
                            new Claim(JwtClaimTypes.Role, "Guest"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("alf created");
            }
            else
            {
                Log.Debug("alf already exists");
            }

        }

    
    }
}
