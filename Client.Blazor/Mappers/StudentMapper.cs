using AutoMapper;
using Client.Blazor.DTOs;
using Shared;

namespace Client.Blazor.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<PaginationRequest, SearchStudentDTO>();

            CreateMap<SearchStudentDTO, PaginationRequest>()
                .ForMember(dest => dest.PageSize, opt => opt.Ignore())
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore());

            CreateMap<StudentProfile, StudentProfileDTO>();
            CreateMap<StudentProfileDTO, StudentProfile>();

            CreateMap<StudentAge, StudentAgeDTO>();
        }
    }
}
