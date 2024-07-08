using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using QLSVConsole.Controllers;
using QLSVConsole.Entities;
using QLSVConsole.Services.ClassService;
using QLSVConsole.Services.StudentService;
using System.Reflection;

namespace QLSVConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IStudentService, StudentService>()
                .AddScoped<IClassService, ClassService>()
                .AddTransient<StudentController>()
                .BuildServiceProvider();

            var studentController = serviceProvider.GetService<StudentController>()!;

            while (true)
            {
                Console.WriteLine("--------------------Quan ly sinh vien--------------------");
                Console.WriteLine("1. Xem danh sach sinh vien");
                Console.WriteLine("2. Them sinh vien");
                Console.WriteLine("3. Sua thong tin sinh vien");
                Console.WriteLine("4. Xoa sinh vien");
                Console.WriteLine("5. Sap xep sinh vien theo ten");
                Console.WriteLine("6. Tim kiem sinh vien theo MSV");
                Console.WriteLine("7. Thoat ung dung");
                Console.WriteLine("------------------------");
                Console.Write("Nhap lua chon: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        studentController.GetAllStudent();
                        break;
                    case 2:
                        studentController.CreateStudent();
                        break;
                    case 3:
                        studentController.UpdateStudent();
                        break;
                    case 4:
                        studentController.DeleteStudent();
                        break;
                    case 5:
                        studentController.SortStudentByName();
                        break;
                    case 6:
                        studentController.SearchStudentByID();
                        break;
                    case 7:
                        return;
                }
            }
        }
    }
}

/*public static List<Teacher> teachers = new List<Teacher>
{
    new () { Id = 1, Name = "Huong", Birthday = DateTime.ParseExact("01/09/1992", "dd/MM/yyyy", null) },
    new () { Id = 2, Name = "Kieu Tuan Dung", Birthday = DateTime.ParseExact("17/07/1983", "dd/MM/yyyy", null) },
    new () { Id = 3, Name = "Ta Quang Chieu", Birthday = DateTime.ParseExact("02/08/1990", "dd/MM/yyyy", null) }
};

private List<Class> classes = new List<Class>
{
    new () { Id = 1, Name = "64KTPM4", Subject = "Lap trinh Windows", Teacher = teachers[0] },
    new () { Id = 2, Name = "64KTPM-NB", Subject = "Nen tang Web", Teacher = teachers[1] }
};*/