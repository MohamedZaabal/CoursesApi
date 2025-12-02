using System.Security.Claims;
using CoursesApi.Data_;
using CoursesApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Authorization
{
    public class PermissionBasedAuthorizationFilter : IAuthorizationFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PermissionBasedAuthorizationFilter(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var requiredPermission = context.ActionDescriptor.EndpointMetadata
                .OfType<CheckPermissionAttribute>()
                .FirstOrDefault()?.PermissionName;


            if (requiredPermission == null)
                return;

            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var dbUser = _userManager.FindByIdAsync(userId).Result;

            if (dbUser == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRoles = _userManager.GetRolesAsync(dbUser).Result;

            var permissions = _context.RolePermissions
                    .Include(rp => rp.Permission)
                    .Include(rp => rp.Role)
                    .Where(rp => userRoles.Contains(rp.Role.Name))
                    .Select(rp => rp.Permission.Name)
                    .ToList();


            if (!permissions.Contains(requiredPermission))
            {
                context.Result = new ForbidResult();
            }
        }


    }
}
        
