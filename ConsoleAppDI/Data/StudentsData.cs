using QLSVConsole.Models;

namespace QLSVConsole.Data
{
    public static class StudentsData
    {
        private static List<Class> classes = ClassesData.Classes;
        public static List<Student> Students = new List<Student>
        {
            new ()
            {
                Id = 1,
                Name = "Ngoc Son",
                Address = "Dong Da",
                Birthday = DateTime.ParseExact("20/02/2004", "dd/MM/yyyy", null),
                /*Birthday = Convert.ToDateTime("20/02/2004", null),*/
                Class = classes[0]
            },
            new ()
            {
                Id = 2,
                Name = "Duy Bach",
                Address = "Gia Lam",
                Birthday = DateTime.ParseExact("24/04/2004", "dd/MM/yyyy", null),
                Class = classes[0]
            }
        };
    }
}
