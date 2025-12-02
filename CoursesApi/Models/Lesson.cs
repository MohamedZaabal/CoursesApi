using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lesson title is required")]
        [MinLength(3, ErrorMessage = "Lesson title must be at least 3 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "VideoUrl is required")]
        [Url(ErrorMessage = "Invalid video URL")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; } // FK
    }
}