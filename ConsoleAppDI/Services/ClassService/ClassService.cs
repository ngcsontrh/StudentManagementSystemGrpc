using QLSVConsole.Data;
using QLSVConsole.Models;

namespace QLSVConsole.Services.ClassService
{
    internal class ClassService : IClassService
    {
        private static List<Class> classes = ClassesData.Classes;

        public bool Any(int id)
        {
            return classes.Any(c => c.Id == id);
        }

        public Class? Get(int classId)
        {
            return classes.FirstOrDefault(c => c.Id == classId);
        }

        public List<Class> GetAll()
        {
            return classes;
        }
    }
}
