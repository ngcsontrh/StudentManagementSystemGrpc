using AutoMapper;
using Client.ConsoleApp.DTOs;
using Shared;

namespace Client.Blazor.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<StudentProfile, StudentProfileDTO>();
            CreateMap<StudentProfileDTO, StudentProfile>();
        }
    }
}
