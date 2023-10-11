using Core.Entities.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user1 = new AppUser
                {
                    DisplayName = "sb",
                    Email = "sb@test.com",
                    UserName = "sb@test.com",
                    Address = new Address
                    {
                        FirstName = "shraddha",
                        LastName = "bhure",
                        Street = "10 The Street",
                        City = "Nagpur",
                        State = "MH",
                        ZipCode = "440033"
                    }
                    
                };

                await userManager.CreateAsync(user1, "Pa$$w0rd"); // Uppercase, lowercase, alphanumeric password

                var user2 = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                };

                await userManager.CreateAsync(user2, "Pa$$w0rd"); // Another password for Bob
            }
        }
    }
}
