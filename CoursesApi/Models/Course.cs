using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models
{
    public class Course
    {
       
        public int Id { get; set; }//pk
        [Required(ErrorMessage ="Title is required")]
        [MinLength(3,ErrorMessage ="title must be at least 3 characters")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "InstructorId is required")]
        public string InstructorId { get; set; }//author of the course

        public List<Lesson> Lessons { get; set; } = new();

    }
}
