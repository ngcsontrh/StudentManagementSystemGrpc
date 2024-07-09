using AutoMapper;
using ProtoBuf.Grpc;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IMapper _mapper;
        public ClassService(IClassRepository classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }
        public async Task<MultipleClassInfosReply> GetAllClassesInfoAsync(Empty request, CallContext context = default)
        {
            MultipleClassInfosReply reply = new MultipleClassInfosReply();
            try
            {
                List<Class>? classes = await _classRepository.GetAllAsync();
                if(classes == null)
                {
                    throw new Exception("There is no classes in database");
                }

                reply.Count = classes.Count;
                reply.Classes = _mapper.Map<List<ClassInfo>>(classes);
            }
            catch (Exception e)
            {
                reply.Message = e.Message;
            }

            return reply;
        }

        public async Task<ClassChart> GetClassChartAsync(Empty request, CallContext callContext = default)
        {
            var chartData = await _classRepository.GetClassChartAsync();
            ClassChart chart = new ClassChart()
            {
                ChartData = _mapper.Map<List<ClassStudentCount>>(chartData)
            };
            return chart;
        }
    }
}
