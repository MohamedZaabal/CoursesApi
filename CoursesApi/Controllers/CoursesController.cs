using System.Security.Claims;
using CoursesApi.Authorization;
using CoursesApi.Filters;
using CoursesApi.Models;
using CoursesApi.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApi.Controllers
{
    //[TypeFilter(typeof(ActionLoggingFilter))]
    //[TypeFilter(typeof(GlobalExceptionFilter))]
    
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;

        }

        [HttpGet]
      
        public ActionResult<List<Course>> GetAll()
        {
            return Ok(_service.GetAll());

        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Course> GetById(int id)
        {
            var course = _service.GetById(id);
            if (course == null)  throw new Exception("Course not found!"); ;

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        [CheckPermission("Course.Create")]
        public ActionResult<Course> Create([FromBody]Course course)
        {
            var instructorId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            course.InstructorId = instructorId;
            var newCourse=_service.Create(course);
            return CreatedAtAction(nameof(GetById), new { id = newCourse.Id }, newCourse);
        }

        [HttpPut("{id}")]
        public ActionResult<Course> Update(int id ,Course course)
        {
            var updated=_service.Update(id, course);
            if(updated== null) return NotFound();

            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var succes=_service.Delete(id);
            if(!succes) return NotFound();
            return NoContent();
        }
            
    }
}
