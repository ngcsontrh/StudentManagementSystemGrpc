using AutoMapper;
using Client.Blazor.DTOs;
using Shared;

namespace Client.Blazor.Mappers
{
    public class ClassMapper : Profile
    {
        public ClassMapper()
        {
            CreateMap<ClassInfo, ClassInfoDTO>();
            CreateMap<ClassInfoDTO, ClassInfo>();

            CreateMap<ClassStudentCount, ClassStudentCountDTO>();
        }
    }
}