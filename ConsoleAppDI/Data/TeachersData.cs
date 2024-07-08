using QLSVConsole.Models;

namespace QLSVConsole.Data
{
    public static class TeachersData
    {
        public static List<Teacher> Teachers { get; set; } = new List<Teacher>
        {
            new Teacher
            {
                Id = 1,
                Name = "Huong",
                Birthday = DateTime.ParseExact("01/09/1992", "dd/MM/yyyy", null),
            },
            new Teacher
            {
                Id = 2,
                Name = "Kieu Tuan Dung",
                Birthday = DateTime.ParseExact("17/07/1983", "dd/MM/yyyy", null),
            },
            new Teacher
            {
                Id = 3,
                Name = "Ta Quang Chieu",
                Birthday = DateTime.ParseExact("02/08/1990", "dd/MM/yyyy", null),
            }
        };
    }
}
