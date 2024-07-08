using QLSVConsole.Entities;

namespace QLSVConsole.Services.StudentService
{
    internal interface IStudentService
    {
        void Add(Student student);
        void Update(Student student);
        void Delete(int studentId);
        Student? Get(int studentId);
        List<Student> GetAll();
        bool Any(int studentId);
    }
}
