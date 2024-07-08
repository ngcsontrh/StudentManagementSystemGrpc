using QLSVConsole.Models;

namespace QLSVConsole.Services.StudentService
{
    internal interface IStudentService
    {
        void Add(Student student);
        void Update(Student student);
        void Delete(int studentId);
        Student? Get(int studentId);
        IEnumerable<Student> GetAll();
        void SortByName();
        bool Any(int studentId);
    }
}
