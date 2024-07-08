using AutoMapper;
using Server.DTOs;
using Server.Entities;
using Shared;

namespace Server.Mappers
{
    public class ClassMapper : Profile
    {
        public ClassMapper()
        {
            CreateMap<Class, ClassInfo>();
            CreateMap<ClassStudentCountDTO, ClassStudentCount>();
        }
    }
}
