using CoursesApi.Models;

namespace CoursesApi.Services.Interface
{
    public interface ICourseService
    {
        List<Course> GetAll();
        Course? GetById(int id);
        Course Create(Course course);
        Course Update(int id,Course course);
        
        bool Delete(int id);   

    }
}
