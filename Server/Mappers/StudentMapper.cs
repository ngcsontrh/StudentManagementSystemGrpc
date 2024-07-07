using AutoMapper;
using Server.DTOs;
using Server.Entities;
using Shared;

namespace Server.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentProfile>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.StudentClass.Id))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.StudentClass.Name))
                .ForMember(dest => dest.ClassSubject, opt => opt.MapFrom(src => src.StudentClass.Subject))
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.StudentClass.ClassTeacher.Id))
                .ForMember(dest => dest.TeacherFullName, opt => opt.MapFrom(src => src.StudentClass.ClassTeacher.FullName))
                .ForMember(dest => dest.TeacherBirthday, opt => opt.MapFrom(src => src.StudentClass.ClassTeacher.Birthday));

            CreateMap<StudentProfile, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForPath(dest => dest.StudentClass.Id, opt => opt.MapFrom(src => src.ClassId))
                .ForPath(dest => dest.StudentClass.Name, opt => opt.MapFrom(src => src.ClassName))
                .ForPath(dest => dest.StudentClass.Subject, opt => opt.MapFrom(src => src.ClassSubject))
                .ForPath(dest => dest.StudentClass.ClassTeacher.Id, opt => opt.MapFrom(src => src.TeacherId))
                .ForPath(dest => dest.StudentClass.ClassTeacher.FullName, opt => opt.MapFrom(src => src.TeacherFullName))
                .ForPath(dest => dest.StudentClass.ClassTeacher.Birthday, opt => opt.MapFrom(src => src.TeacherBirthday));

            CreateMap<PaginationRequest, SearchStudentDTO>();

            CreateMap<SearchStudentDTO, PaginationRequest>()
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            CreateMap<StudentAgeChartDTO, StudentAge>();
        }
    }
}
