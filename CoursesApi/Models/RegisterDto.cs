namespace CoursesApi.Models
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }   // Admin - Instructor - Student
        public string FullName { get; set; }//New

    }


    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

}
