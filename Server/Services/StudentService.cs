﻿using ProtoBuf.Grpc;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared;
using Shared.DTOs;

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
                reply.Students = students.Select(s => new StudentProfile
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Birthday = s.Birthday,
                    Address = s.Address,
                    ClassId = s.StudentClass.Id,
                    ClassName = s.StudentClass.Name,
                    ClassSubject = s.StudentClass.Subject,
                    TeacherId = s.StudentClass.ClassTeacher.Id,
                    TeacherFullName = s.StudentClass.ClassTeacher.FullName,
                    TeacherBirthday = s.StudentClass.ClassTeacher.Birthday
                }).ToList();
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

                reply.Student = new StudentProfile()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Birthday = student.Birthday,
                    Address = student.Address,
                    ClassId = student.StudentClass.Id,
                    ClassName = student.StudentClass.Name,
                    ClassSubject = student.StudentClass.Subject,
                    TeacherId = student.StudentClass.ClassTeacher.Id,
                    TeacherFullName = student.StudentClass.ClassTeacher.FullName,
                    TeacherBirthday = student.StudentClass.ClassTeacher.Birthday
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
                reply.Count = count;

                if (students == null || students.Count == 0)
                {
                    throw new Exception("There is no student in this page");
                }
                reply.Students = new List<StudentProfile>();

                foreach (var student in students)
                {
                    reply.Students.Add(new StudentProfile
                    {
                        Id = student.Id,
                        FullName = student.FullName,
                        Birthday = student.Birthday,
                        Address = student.Address,
                        ClassId = student.StudentClass.Id,
                        ClassName = student.StudentClass.Name,
                        ClassSubject = student.StudentClass.Subject,
                        TeacherId = student.StudentClass.ClassTeacher.Id,
                        TeacherFullName = student.StudentClass.ClassTeacher.FullName,
                        TeacherBirthday = student.StudentClass.ClassTeacher.Birthday
                    });
                }
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<MultipleStudentProfilesReply> SearchStudentAsync(SearchRequest request, CallContext callContext = default)
        {
            var reply = new MultipleStudentProfilesReply();
            try
            {
                var studentField = new SearchStudentDTO
                {
                    Id = request.Id,
                    Name = request.Name,
                    Address = request.Address,
                    ClassId = request.ClassId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };
                List<Student>? students = await _studentRepository.SearchAsync(studentField);
                if (students == null || students.Count == 0)
                {
                    throw new Exception("There is no students");
                }
                else
                {
                    reply.Students = new List<StudentProfile>();
                    reply.Count = reply.Students.Count;
                    foreach (var student in students)
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
            }
            catch (Exception ex)
            {
                reply.Message = ex.Message;
            }

            return reply;
        }

        public async Task<MultipleStudentProfilesReply> SearchStudentWithPagination(SearchStudentWithPaginationRequest request, CallContext callContext = default)
        {
            var reply = new MultipleStudentProfilesReply();
            try
            {
                var studentField = new SearchStudentDTO
                {
                    Id = request.Id,
                    Name = request.Name,
                    Address = request.Address,
                    ClassId = request.ClassId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate
                };
                List<Student>? students = await _studentRepository.SearchWithPaginationAsync(studentField, pageSize: request.PageSize, pagenNumber: request.PageNumber);
                int count = await _studentRepository.CountWithSearch(studentField);
                if (count == 0)
                {
                    throw new Exception("There is no student in the database");
                }

                if (students == null || students.Count == 0)
                {
                    throw new Exception("There is no student in this page");
                }
                reply.Students = new List<StudentProfile>();
                reply.Count = count;
                reply.Students = students.Select(student => new StudentProfile
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Birthday = student.Birthday,
                    Address = student.Address,
                    ClassId = student.StudentClass.Id,
                    ClassName = student.StudentClass.Name,
                    ClassSubject = student.StudentClass.Subject,
                    TeacherId = student.StudentClass.ClassTeacher.Id,
                    TeacherFullName = student.StudentClass.ClassTeacher.FullName,
                    TeacherBirthday = student.StudentClass.ClassTeacher.Birthday
                }).ToList();
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
    }
}
