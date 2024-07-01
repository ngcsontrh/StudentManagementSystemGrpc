using ProtoBuf.Grpc;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public StudentService(IClassRepository classRepository, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public async Task<OperationReply> CreateAsync(CreateStudentRequest request, CallContext context = default)
        {
            OperationReply reply = new OperationReply();
            try
            {
                Class? clazz = await _classRepository.GetAsync(request.ClassId);
                if (clazz == null)
                {
                    throw new Exception($"There is no class id = {request.ClassId}");
                }

                Student student = new Student
                {
                    FullName = request.FullName,
                    Birthday = request.Birthday,
                    Address = request.Address,
                    StudentClass = clazz
                };
                await _studentRepository.CreateAsync(student);

                reply.Success = true;
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<OperationReply> DeleteAsync(IdRequest request, CallContext context = default)
        {
            OperationReply reply = new OperationReply(); ;
            try
            {
                Student? student = await _studentRepository.GetAsync(request.Id);
                if (student == null)
                {
                    throw new Exception($"There is no student id = {request.Id}");
                }

                await _studentRepository.DeleteAsync(student);
                reply.Success = true;
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }
            return reply;
        }

        public async Task<MultipleStudentProfilesReply> GetAllProfilesAsync(Empty request, CallContext context = default)
        {
            MultipleStudentProfilesReply reply = new MultipleStudentProfilesReply();
            try
            {
                List<Student>? students = await _studentRepository.GetAllAsync();
                if (students == null)
                {
                    throw new Exception("There is no student in database");
                }

                reply.Count = students.Count;
                reply.Students = new List<StudentProfile>();
                foreach (Student student in students)
                {
                    reply.Students.Add(new StudentProfile
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Birthday = student.Birthday,
                        Address = student.Address,
                        ClassId = student.StudentClass.Id,
                        ClassName = student.StudentClass.Name
                    });
                }
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<StudentDetailsReply> GetDetailsAsync(IdRequest request, CallContext context = default)
        {
            StudentDetailsReply reply = new StudentDetailsReply();
            try
            {
                Student? student = await _studentRepository.GetDetailsAsync(request.Id);
                if(student == null)
                {
                    throw new Exception($"There is no student id = {request.Id}");
                }

                reply.Student = new StudentDetails
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Birthday = student.Birthday,
                    Address = student.Address,
                    ClassId = student.StudentClass.Id,
                    ClassName = student.StudentClass.Name,
                    ClassSubject = student.StudentClass.Name,
                    TeacherId = student.StudentClass.ClassTeacher.Id,
                    TeacherBirthday = student.StudentClass.ClassTeacher.Birthday,
                    TeacherFullName = student.StudentClass.ClassTeacher.FullName
                };
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<StudentProfileReply> GetProfileAsync(IdRequest request, CallContext context = default)
        {
            StudentProfileReply reply = new StudentProfileReply();
            try
            {
                Student? student = await _studentRepository.GetAsync(request.Id);
                if(student == null)
                {
                    throw new Exception($"There is no student id = {request.Id}");
                }

                reply.Student = new StudentProfile()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Birthday = student.Birthday,
                    Address = student.Address,
                    ClassId = student.StudentClass.Id,
                    ClassName = student.StudentClass.Name,
                };
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }
            return reply;
        }

        public async Task<MultipleStudentProfilesReply> GetWithPaginationAsync(PaginationRequest request, CallContext context = default)
        {
            MultipleStudentProfilesReply reply = new MultipleStudentProfilesReply();
            try
            {
                List<Student>? students = await _studentRepository.GetWithPaginationAsync(request.PageNumber, request.PageSize);
                int count = await _studentRepository.CountAsync();

                if(count == 0)
                {
                    throw new Exception("There is no student in database");
                }

                if(students == null || students.Count == 0)
                {
                    throw new Exception("There is no student in this page");
                }

                reply.Count = count;
                reply.Students = new List<StudentProfile>();
                foreach(var student in students)
                {
                    reply.Students.Add(new StudentProfile
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Birthday = student.Birthday,
                        Address = student.Address,
                        ClassId = student.StudentClass.Id,
                        ClassName = student.StudentClass.Name
                    });
                }
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<OperationReply> UpdateAsync(UpdateStudentRequest request, CallContext context = default)
        {
            OperationReply reply = new OperationReply();
            try
            {
                Class? clazz = await _classRepository.GetAsync(request.ClassId);
                if (clazz == null)
                {
                    throw new Exception($"There is no class id = {request.ClassId}");
                }

                Student? student = await _studentRepository.GetAsync(request.Id);
                if (student == null)
                {
                    throw new Exception($"There is no student id = {request.Id}");
                }
                student.FullName = request.FullName;
                student.Birthday = request.Birthday;
                student.Address = request.Address;
                student.StudentClass = clazz;
                await _studentRepository.UpdateAsync(student);

                reply.Success = true;
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }
    }
}
