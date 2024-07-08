using Microsoft.Extensions.DependencyInjection;
using QLSVConsole.Controllers;
using QLSVConsole.Services.ClassService;
using QLSVConsole.Services.StudentService;
using QLSVConsole.Services.TeacherService;

namespace QLSVConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITeacherService, TeacherService>()
                .AddSingleton<IClassService, ClassService>()
                .AddSingleton<IStudentService, StudentService>()
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
