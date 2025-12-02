using Microsoft.AspNetCore.Identity;

namespace CoursesApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; internal set; }
    }
}
