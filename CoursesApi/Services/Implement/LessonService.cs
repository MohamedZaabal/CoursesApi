using CoursesApi.Data_;
using CoursesApi.Models;
using CoursesApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Services.Implement
{
    public class LessonService : ILessonService
    {
        
        private readonly ApplicationDbContext _context;

        

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<Lesson?> GetByIdAsync(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }

        
        public Lesson? GetById(int id)
        {
            return _context.Lessons.Find(id);
        }

        public Lesson Create(int courseId, Lesson lesson)
        {
            
            var course = _context.Courses.Find(courseId);
            if (course == null)
                throw new Exception($"Course with id {courseId} not found");

            
            lesson.CourseId = courseId;

            
            _context.Lessons.Add(lesson);
            _context.SaveChanges(); 

            return lesson;
        }

        public List<Lesson> GetLessonsByCourse(int courseId)
        {
            
            var courseExists = _context.Courses.Any(c => c.Id == courseId);
            if (!courseExists)
                return new List<Lesson>();

            return _context.Lessons
                .Where(x => x.CourseId == courseId)
                .ToList();
        }

        public Lesson? Update(int id, Lesson updated)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null) return null;

            
            if (lesson.CourseId != updated.CourseId)
                throw new Exception("Cannot change CourseId for existing lesson");

            lesson.Title = updated.Title;
            lesson.VideoUrl = updated.VideoUrl;

            _context.SaveChanges(); 

            return lesson;
        }

        public bool Delete(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null) return false;

            _context.Lessons.Remove(lesson);
            _context.SaveChanges(); 

            return true;
        }
    }
}