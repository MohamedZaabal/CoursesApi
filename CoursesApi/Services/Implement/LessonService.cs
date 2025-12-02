using CoursesApi.Models;
using CoursesApi.Services.Interface;

namespace CoursesApi.Services.Implement
{
    public class LessonService : ILessonService
    {
        private readonly List<Lesson> _lessons=new();
        private int _nextId = 1;
        public Lesson Create(int courseId, Lesson lesson)
        {
            lesson.Id= _nextId++;
            lesson.CourseId= courseId;
            _lessons.Add(lesson);
            return lesson;

            
        }

        public bool Delete(int id)
        {
           var lesson=GetById(id);
            if(lesson==null) return false;
            
            _lessons.Remove(lesson);
            return true;
        }

        public Lesson? GetById(int Id)
        {
           return _lessons.FirstOrDefault(x => x.Id == Id);
        }

        public List<Lesson> GetLessonByCourse(int courseId)
        {
            return _lessons.Where(x=>x.CourseId== courseId).ToList();
        }

        public Lesson? Update(int id, Lesson updated)
        {
            var lessons=GetById(id);
            if(lessons==null) return null;
            
            //map 

            lessons.Title= updated.Title;
            lessons.VideoUrl= updated.VideoUrl;
            return lessons;
           
        }
    }
}
