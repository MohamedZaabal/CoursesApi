using CoursesApi.Models;

namespace CoursesApi.Services.Interface
{
    public interface ILessonService
    {
        List<Lesson> GetLessonByCourse(int  courseId);
        Lesson? GetById(int Id);
        Lesson Create(int  courseId,Lesson lesson);
        Lesson? Update(int id, Lesson updated);
        bool Delete(int id);    

    }

   
}
