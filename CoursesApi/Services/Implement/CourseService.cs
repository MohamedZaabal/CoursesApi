using CoursesApi.Data_;
using CoursesApi.Models;
using CoursesApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Course> GetAll()
    {
        return _context.Courses
            .Include(c => c.Lessons)
            .ToList();
    }

    public Course? GetById(int id)
    {
        return _context.Courses
            .Include(c => c.Lessons) 
            .FirstOrDefault(x => x.Id == id);
    }

    public Course Create(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges(); 
        return course;
    }

    public Course? Update(int id, Course updated)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return null;

        
        course.Title = updated.Title;
        course.Description = updated.Description;
        course.InstructorId = updated.InstructorId;

        _context.SaveChanges();
        return course;
    }

    public bool Delete(int id)
    {
        var course = _context.Courses.Find(id);
        if (course == null) return false;

        _context.Courses.Remove(course);
        _context.SaveChanges();
        return true;
    }
}