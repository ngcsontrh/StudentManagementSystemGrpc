using AutoMapper;
using ProtoBuf.Grpc;
using Server.DTOs;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;

        public StudentService(IClassRepository classRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _mapper = mapper;
        }
        public async Task<OperationReply> CreateAsync(StudentProfile request, CallContext context = default)
        {
            OperationReply reply = new OperationReply();
            try
            {
                Class? clazz = await _classRepository.GetAsync(request.ClassId);
                if (clazz == null)
                {
                    throw new Exception($"There is no class id = {request.ClassId}");
                }

                Student student = _mapper.Map<Student>(request);
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
                reply.Students = _mapper.Map<List<StudentProfile>>(students);
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
                if (student == null)
                {
                    throw new Exception($"There is no student id = {request.Id}");
                }

                reply.Student = _mapper.Map<StudentProfile>(student);
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }
            return reply;
        }

        public async Task<MultipleStudentProfilesReply> GetPaginationAsync(PaginationRequest request, CallContext callContext = default)
        {
            var reply = new MultipleStudentProfilesReply();
            try
            {
                SearchStudentDTO studentField = _mapper.Map<SearchStudentDTO>(request);
                var searchResult = await _studentRepository.GetPaginationAsync(studentField, pageSize: request.PageSize, pagenNumber: request.PageNumber);
                reply.Count = searchResult.Total;
                if (reply.Count == 0)
                {
                    throw new Exception("There is no student");
                }
                reply.Students = _mapper.Map<List<StudentProfile>>(searchResult.Students);
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<OperationReply> UpdateAsync(StudentProfile request, CallContext context = default)
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

        public async Task<StudentAgeChart> GetStudentAgeChartAsync(IdRequest classIdRequest, CallContext callContext = default)
        {
            var chartData = await _studentRepository.GetStudentAgesChartAsync(classIdRequest.Id);
            StudentAgeChart studentAgeChart = new StudentAgeChart
            {
                ChartData = _mapper.Map<List<StudentAge>>(chartData)
            };

            return studentAgeChart;
        }

        public async Task<StudentCount> GetStudentCountAsync(Empty request, CallContext callContext = default)
        {
            int total = await _studentRepository.CountAsync();
            StudentCount result = new StudentCount
            {
                Total = total
            };
            return result;
        }
    }
}   
