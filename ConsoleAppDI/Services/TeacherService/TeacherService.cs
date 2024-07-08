using QLSVConsole.Data;
using QLSVConsole.Models;

namespace QLSVConsole.Services.TeacherService
{
    internal class TeacherService : ITeacherService
    {
        private static List<Teacher> teachers = TeachersData.Teachers;
        public Teacher? Get(int teacherId)
        {
            return teachers.FirstOrDefault(t => t.Id == teacherId);
        }

        public List<Teacher> GetAll()
        {
            return teachers;
        }
    }
}
