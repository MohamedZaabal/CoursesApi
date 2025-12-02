namespace CoursesApi.Models
{

    public class User
    {
        public string Id { get; set; } // ممكن ندي Guid
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin / Instructor / Student
    }
}


