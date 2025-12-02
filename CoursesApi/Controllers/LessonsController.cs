using CoursesApi.Models;
using CoursesApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonsController( ILessonService lessonService)
        {
            _lessonService = lessonService;
            
        }

        [HttpGet]
        public ActionResult<List<Lesson>> GetLessons(int courseId)
        {
            return Ok(_lessonService.GetLessonsByCourse(courseId));
        }

        [HttpGet("{id}")]
        public  async Task<ActionResult<Lesson>> GetLesson(int courseId, int id)
        {
            var lesson =  await _lessonService.GetByIdAsync(id);
            if (lesson == null || lesson.CourseId != courseId) return NotFound();
             
            return Ok(lesson);
        }
        [HttpPost]
        public ActionResult<Lesson> CreateLesson(int courseId, Lesson lesson)
        {
            var newLesson = _lessonService.Create(courseId, lesson);
            return CreatedAtAction(nameof(GetLesson), new { courseId = courseId, id = newLesson.Id }, newLesson);
        }

        [HttpPut("{id}")]
        public ActionResult<Lesson> UpdateLesson(int courseId, int id, Lesson lesson)
        {
            var updated = _lessonService.Update(id, lesson);
            if (updated == null || updated.CourseId != courseId)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLesson(int courseId, int id)
        {
            var success = _lessonService.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
