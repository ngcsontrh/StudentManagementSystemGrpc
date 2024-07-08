using QLSVConsole.Models;

namespace QLSVConsole.Services.TeacherService
{
    internal interface ITeacherService
    {
        Teacher? Get(int teacherId);
        List<Teacher> GetAll();
    }
}
