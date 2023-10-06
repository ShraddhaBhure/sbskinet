
using Core.Entities.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
              var user = new AppUser
              {
                DisplayName ="sb",
                Email = "sb@test.com",
                UserName = "sb@test.com",
                Address = new Address
                { 
                    FirstName ="shraddha",
                    LastName ="bhure",
                    Street="10 The Street",
                    City="Nagpur",
                    State="MH",
                    ZipCode="440033"
                }

              };
              await userManager.CreateAsync(user, "Pa$$w0rd"); //upercase_lowercase_aphanumeric Charactors set for password


            }

        }

        
    }
}