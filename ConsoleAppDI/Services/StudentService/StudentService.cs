using QLSVConsole.Data;
using QLSVConsole.Models;

namespace QLSVConsole.Services.StudentService
{
    internal class StudentService : IStudentService
    {
        private static List<Student> students = StudentsData.Students;
        public void Add(Student student)
        {
            students.Add(student);
        }

        public void Delete(int studentId)
        {
            Student student = students.FirstOrDefault(s => s.Id == studentId)!;
            students.Remove(student);
        }

        public Student? Get(int studentId)
        {
            return students.FirstOrDefault(s => s.Id == studentId);
        }

        public IEnumerable<Student> GetAll()
        {
            return students;
        }

        public void Update(Student student)
        {
            var currentStudent = students.FirstOrDefault(s => s.Id == student.Id)!;
            currentStudent.Name = student.Name;
            currentStudent.Birthday = student.Birthday;
            currentStudent.Address = student.Address;
            currentStudent.Class = student.Class;
        }

        public bool Any(int studentId)
        {
            return students.Any(s => s.Id == studentId);
        }

        public void SortByName()
        {
            students = students.OrderBy(s => s.Name).ToList();
        }
    }
}
