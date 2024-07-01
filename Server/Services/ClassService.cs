using ProtoBuf.Grpc;
using Server.Entities;
using Server.Repositories.Interfaces;
using Shared;

namespace Server.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        public async Task<MultipleClassInfosReply> GetAllClassesInfo(Empty request, CallContext context = default)
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
                reply.Classes = new List<ClassInfo>();
                foreach (Class clazz in classes)
                {
                    reply.Classes.Add(new ClassInfo
                    {
                        Id = clazz.Id,
                        Name = clazz.Name,
                        Subject = clazz.Subject,
                    });
                }
            }
            catch (Exception e)
            {
                reply.Message = e.Message;
            }

            return reply;
        }
    }
}
