using Microsoft.AspNetCore.Authorization;

namespace CoursesApi.Authorization
{
    
    public class CheckPermissionAttribute : Attribute
    {
        public string PermissionName { get; }

        public CheckPermissionAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}

