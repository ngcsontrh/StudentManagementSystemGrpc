using AutoMapper;
using Client.ConsoleApp.DTOs;
using Grpc.Core;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ConsoleApp.Controllers
{
    internal class StudentController
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IClassService classService, IMapper mapper)
        {
            _studentService = studentService;
            _classService = classService;
            _mapper = mapper;
        }

        public async Task Index()
        {
            while(true)
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
                        await GetAllProfilesAsync();
                        break;
                    case 2:
                        await AddNewStudent();
                        break;
                    case 3:
                        await UpdateStudent();
                        break;
                    case 4:
                        await DeleteStudent();
                        break;
                    case 5:
                        await GetAllProfilesAsync(sortByName:true);
                        break;
                    case 6:
                        await GetStudentDetailsById();
                        break;
                    case 7:
                        return;
                }
            }
        }

        public async Task GetAllProfilesAsync(bool sortByName = false)
        {
            var reply = await _studentService.GetAllProfilesAsync(new Empty());

            if (reply.Students != null)
            {
                List<StudentProfileDTO> students = _mapper.Map<List<StudentProfileDTO>>(reply.Students);
                if(sortByName)
                {
                    students = students.OrderBy(s => s.FullName).ToList();
                }
                foreach (var student in students)
                {
                    Console.WriteLine($"Student Id: {student.Id}");
                    Console.WriteLine($"Full Name: {student.FullName}");
                    Console.WriteLine($"Birthday: {student.Birthday.ToString("dd/MM/yyyy")}");
                    Console.WriteLine($"Address: {student.Address}");
                    Console.WriteLine($"Class Name: {student.ClassName}");
                    Console.WriteLine("--------------");
                }
            }
            else
            {
                Console.WriteLine($"Error: {reply.Message}");
            }
        }

        public async Task AddNewStudent()
        {
            StudentProfileDTO student = new StudentProfileDTO();
            Console.Write($"Nhap ten sinh vien: ");
            student.FullName = Console.ReadLine()!;

            Console.Write("Nhap ngay sinh sinh vien: ");
            student.Birthday = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);

            Console.Write("Nhap dia chi hoc sinh: ");
            student.Address = Console.ReadLine()!;

            Console.Write("Nhap ma lop hoc: ");
            student.ClassId = Convert.ToInt32(Console.ReadLine());
            
            var request = _mapper.Map<StudentProfile>(student);
            var reply = await _studentService.CreateAsync(request);
            if(reply.Success)
            {
                Console.WriteLine("Da them moi sinh vien");
            }
            else
            {
                Console.WriteLine(reply.Message);
            }
        }

        public async Task UpdateStudent()
        {
            Console.Write("Nhap MSV sinh vien can chinh sua: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var studentReply = await _studentService.GetProfileAsync(new IdRequest { Id = id });
            if(studentReply.Student == null)
            {
                Console.WriteLine(studentReply.Message);    
                return;
            }

            StudentProfileDTO student = _mapper.Map<StudentProfileDTO>(studentReply.Student);

            Console.Write("Nhap ho ten: ");
            student.FullName = Console.ReadLine()!;

            Console.Write("Nhap ngay sinh hoc sinh: ");
            student.Birthday = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);

            Console.Write("Nhap dia chi hoc sinh: ");
            student.Address = Console.ReadLine()!;

            Console.Write("Nhap ma lop hoc: ");
            student.ClassId = Convert.ToInt32(Console.ReadLine());

            var request = _mapper.Map<StudentProfile>(student);
            var reply = await _studentService.UpdateAsync(request);
            if(reply.Success)
            {
                Console.WriteLine("Da cap nhat sinh vien");
            }
            else
            {
                Console.WriteLine(reply.Message);
            }
        }

        public async Task DeleteStudent()
        {
            Console.Write("Nhap MSV sinh vien can xoa: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());
            var reply = await _studentService.DeleteAsync(new IdRequest { Id = deleteId });
            if (reply.Success)
            {
                Console.WriteLine($"Da xoa sinh vien co MSV {deleteId}");
            }
            else
            {
                Console.WriteLine(reply.Message);
            }
        }

        public async Task GetStudentDetailsById()
        {
            Console.Write("Nhap Id sinh vien can tim kiem: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var reply = await _studentService.GetProfileAsync(new IdRequest { Id = id });
            if(reply.Student != null)
            {
                StudentProfileDTO student = _mapper.Map<StudentProfileDTO>(reply.Student);
                Console.WriteLine($"Student Id: {student.Id}");
                Console.WriteLine($"Full Name: {student.FullName}");
                Console.WriteLine($"Birthday: {student.Birthday.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Address: {student.Address}");

                Console.WriteLine($"Class Id: {student.ClassId}");
                Console.WriteLine($"Class Name: {student.ClassName}");
                Console.WriteLine($"Class Subject: {student.ClassSubject}");

                Console.WriteLine($"Teacher Id: {student.TeacherId}");
                Console.WriteLine($"Teacher Full Name: {student.TeacherFullName}");
                Console.WriteLine($"Teacher Birthday: {student.TeacherBirthday}");
            }
        }
    }
}
