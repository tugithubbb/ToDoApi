using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Data.Seeders
{
    public class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Admin", "User" };

            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole(role)
                    {
                        NormalizedName = role.ToUpper() 
                    };

                    await roleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
