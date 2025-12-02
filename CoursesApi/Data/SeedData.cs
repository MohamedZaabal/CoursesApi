using CoursesApi.Data_;
using CoursesApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Data
{
    public static class SeedData
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> rolemanager)
        {
            string[] roles = { "Admin", "Instructor", "Student" };
            foreach (var role in roles)
            {
                if (!await rolemanager.RoleExistsAsync(role))
                {
                    await rolemanager.CreateAsync(new IdentityRole(role));
                }


            }
        }

        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@system.com";
            string adminPassword = "Admin@123";
            var existingAdmin=await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin"
                };

                var result =await userManager.CreateAsync(admin,adminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
       
    
        public static async Task SeedPermissions(ApplicationDbContext context)
        {
            if(!context.Permissions.Any())
            {
                var permissions = new List<Permission>
                {
                    new Permission { Name = "Course.View" },
                    new Permission { Name = "Course.Create" },
                    new Permission { Name = "Course.Update" },
                    new Permission { Name = "Course.Delete" },
                    new Permission { Name = "User.View" },
                    new Permission { Name = "User.Manage" },
                    new Permission { Name = "Role.Manage" }
                };
                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }
        }


        public static async Task SeedRolePermissions(ApplicationDbContext context)
        {
            if (!context.RolePermissions.Any())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                var instructorRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Instructor");

                var allPermissions = await context.Permissions.ToListAsync();

                var rolePermissions = new List<RolePermission>();

                // Admin عنده كل الصلاحيات
                foreach (var perm in allPermissions)
                {
                    rolePermissions.Add(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = perm.Id
                    });
                }

                // Instructor عنده بعض الصلاحيات
                var instructorPerms = allPermissions.Where(p => p.Name.StartsWith("Course")).ToList();
                foreach (var perm in instructorPerms)
                {
                    rolePermissions.Add(new RolePermission
                    {
                        RoleId = instructorRole.Id,
                        PermissionId = perm.Id
                    });
                }

                context.RolePermissions.AddRange(rolePermissions);
                await context.SaveChangesAsync();
            }
        }



    }

}
