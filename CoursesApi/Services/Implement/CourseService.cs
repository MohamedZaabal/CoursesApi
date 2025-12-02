using CoursesApi.Models;
using CoursesApi.Services.Interface;

namespace CoursesApi.Services.Implement
{
    public class CourseService : ICourseService
    {
        private readonly List<Course> _courses=new();
        private int _nextId = 1;

      
        public Course Create(Course course)
        {
           course.Id= _nextId++;
            _courses.Add(course);
            return course;
        }

        public bool Delete(int id)
        {
           var course = GetById(id);
            if (course == null) return false;

            _courses.Remove(course);
            return true;
            
        }

        public List<Course> GetAll()
        {
          return _courses;
        }

        public Course? GetById(int id)
        {
            return _courses.FirstOrDefault(x=>x.Id == id);
        }

        public Course Update(int id, Course updated)
        {
            var course= GetById(id);
            if (course == null) return null;
            //map
            course.Title= updated.Title;
            course.Description= updated.Description;
            course.InstructorId= updated.InstructorId;
           return course;
        }

        // عشان اجرب ال ميدل وير

       

    }
}
