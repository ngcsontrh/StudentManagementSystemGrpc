using QLSVConsole.Models;

namespace QLSVConsole.Data
{
    public static class ClassesData
    {
        private static List<Teacher> teachers = TeachersData.Teachers;
        public static List<Class> Classes { get; set; } = new List<Class>
        {
            new ()
            {
                Id = 1,
                Name = "64KTPM4",
                Subject = "Lap trinh Windows",
                Teacher = teachers[0],
            },
            new ()
            {
                Id = 2,
                Name = "64KTPM-NB",
                Subject = "Nen tang Web",
                Teacher = teachers[1],
            }
        };
    }
}
