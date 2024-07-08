using QLSVConsole.Models;
using QLSVConsole.Services.ClassService;
using QLSVConsole.Services.StudentService;
using QLSVConsole.Services.TeacherService;
using System.Globalization;

namespace QLSVConsole.Controllers
{
    internal class StudentController
    {
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public StudentController(IClassService classService, IStudentService studentService, ITeacherService teacherService)
        {
            _classService = classService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public void GetAllStudent()
        {
            int index = 1;
            foreach (var student in _studentService.GetAll())
            {
                Console.Write($"=> STT {index++}\n");
                Console.Write($"MSV: {student.Id}\t");
                Console.Write($"|Ten sinh vien: {student.Name}\t");
                Console.Write($"|Ngay sinh: {student.Birthday.ToString("dd/MM/yyyy")}\t");
                Console.Write($"|Dia chi: {student.Address}\n");
                Console.Write($"Ma lop: {student.Class!.Id}\t");
                Console.Write($"|Ten lop: {student.Class.Name}\t");
                Console.Write($"|Mon hoc: {student.Class.Subject}\n");
                Console.Write($"Ma giao vien: {student.Class.Teacher!.Id}\t");
                Console.Write($"|Ten giao vien: {student.Class.Teacher.Name}\t");
                Console.Write($"|Ngay sinh giao vien: {student.Class.Teacher.Birthday.ToString("dd/MM/yyyy")}\n");
                Console.WriteLine("-----------------------------------\n");
            }
        }

        public void CreateStudent()
        {
            Student newStd = new Student();
            while (true)
            {
                Console.Write("Nhap msv hoc sinh: ");
                newStd.Id = Convert.ToInt32(Console.ReadLine());
                bool existStudent = _studentService.Any(newStd.Id);
                if (existStudent)
                {
                    Console.WriteLine("Sinh vien da ton tai!");
                    return;
                }
                else
                {
                    break;
                }
            }

            Console.Write("Nhap ten hoc sinh: ");
            newStd.Name = Console.ReadLine();

            string stdStringDate;
            while (true)
            {
                try
                {
                    Console.Write("Nhap ngay sinh hoc sinh: ");
                    stdStringDate = Console.ReadLine()!;
                    newStd.Birthday = DateTime.ParseExact(stdStringDate, "dd/MM/yyyy", null);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Dinh dang khong hop le! Vui long nhap lai!");
                }
            }

            Console.Write("Nhap dia chi hoc sinh: ");
            newStd.Address = Console.ReadLine();

            int classId;
            while (true)
            {
                Console.Write("Nhap ma lop hoc: ");
                classId = Convert.ToInt32(Console.ReadLine());
                if (_classService.Any(classId))
                {
                    newStd.Class = _classService.Get(classId);
                    break;
                }
                Console.WriteLine("Ma lop khong ton tai! Vui long nhap lai");
            }

            _studentService.Add(newStd);

            Console.WriteLine("Da them thong tin sinh vien\n\n");
        }

        public void UpdateStudent()
        {
            Console.WriteLine("Nhap MSV sinh vien can chinh sua");
            int id = Convert.ToInt32(Console.ReadLine());
            var student = _studentService.Get(id);

            if (student == null)
            {
                Console.WriteLine($"Khong ton tai sinh vien co MSV {id}");
                return;
            }

            Console.WriteLine("Bo trong neu khong muon chinh sua");

            Console.Write("Nhap ho ten: ");
            string? stdName = Console.ReadLine();
            if (!string.IsNullOrEmpty(stdName)) { student.Name = stdName; }

            string? stdStringDate;
            while (true)
            {
                try
                {
                    Console.Write("Nhap ngay sinh hoc sinh: ");
                    stdStringDate = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(stdStringDate)) { break; }

                    student.Birthday = DateTime.ParseExact(stdStringDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Dinh dang khong hop le! Vui long nhap lai!");
                }
            }

            Console.Write("Nhap dia chi hoc sinh: ");
            string? stdAddress = Console.ReadLine();
            if (!string.IsNullOrEmpty(stdAddress)) { student.Address = stdAddress; }

            string? strClassId;
            int classId;
            while (true)
            {
                Console.Write("Nhap ma lop hoc: ");
                strClassId = Console.ReadLine();

                if (string.IsNullOrEmpty(strClassId)) { break; }

                classId = Convert.ToInt32(strClassId);
                if (_classService.Any(classId))
                {
                    student.Class = _classService.Get(classId);
                    break;
                }

                Console.WriteLine("Ma lop khong ton tai! Vui long nhap lai");
            }

            Console.WriteLine("Da cap nhat thong sin sinh vien");
        }

        public void DeleteStudent()
        {
            Console.WriteLine("Nhap MSV sinh vien can xoa");
            int id = Convert.ToInt32(Console.ReadLine());

            if (!_studentService.Any(id))
            {
                Console.WriteLine($"Khong ton tai sinh vien co MSV {id}");
                return;
            }

            _studentService.Delete(id);
            Console.WriteLine($"Da xoa sinh vien co MSV {id}");
        }

        public void SortStudentByName()
        {
            _studentService.SortByName();
            Console.WriteLine("Da sap xep danh sach sinh vien theo ten");
        }

        public void SearchStudentByID()
        {
            Console.WriteLine("Nhap MSV sinh vien can tim");
            int id = Convert.ToInt32(Console.ReadLine());

            var student = _studentService.Get(id);
            if (student == null)
            {
                Console.WriteLine("Khong ton tai sinh vien!");
                return;
            }

            Console.Write($"MSV: {student.Id}\t");
            Console.Write($"|Ten sinh vien: {student.Name}\t");
            Console.Write($"|Ngay sinh: {student.Birthday.ToString("dd/MM/yyyy")}\t");
            Console.Write($"|Dia chi: {student.Address}\n");
            Console.Write($"Ma lop: {student.Class!.Id}\t");
            Console.Write($"|Ten lop: {student.Class.Name}\t");
            Console.Write($"|Mon hoc: {student.Class.Subject}\n");
            Console.Write($"Ma giao vien: {student.Class.Teacher!.Id}\t");
            Console.Write($"|Ten giao vien: {student.Class.Teacher.Name}\t");
            Console.Write($"|Ngay sinh giao vien: {student.Class.Teacher.Birthday.ToString("dd/MM/yyyy")}\n");
            Console.WriteLine("-----------------------------------\n");
        }
    }
}
