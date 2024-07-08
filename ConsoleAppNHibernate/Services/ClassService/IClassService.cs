using QLSVConsole.Entities;

namespace QLSVConsole.Services.ClassService
{
    internal interface IClassService
    {
        Class? Get(int classId);
        List<Class> GetAll();
        bool Any(int id);
    }
}
