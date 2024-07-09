using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IClassService
    {
        [OperationContract]
        Task<MultipleClassInfosReply> GetAllClassesInfoAsync(Empty request, CallContext context = default);

        [OperationContract]
        Task<ClassChart> GetClassChartAsync(Empty request, CallContext callContext = default);
    }

    [DataContract]
    public class ClassStudentCount
    {
        [DataMember(Order = 1)] public string ClassName { get; set; } = null!;
        [DataMember(Order = 2)] public double StudentPercentage { get; set; }
    }

    [DataContract]
    public class ClassChart
    {
        [DataMember(Order = 1)] public List<ClassStudentCount> ChartData { get; set; } = null!;
    }

    [DataContract]
    public class ClassInfo
    {
        [DataMember(Order = 1)] public int Id { get; set; }

        [DataMember(Order = 2)] public string Name { get; set; } = null!;

        [DataMember(Order = 3)] public string Subject { get; set; } = null!;
    }

    [DataContract]
    public class MultipleClassInfosReply
    {
        [DataMember(Order = 1)] public List<ClassInfo>? Classes { get; set; }

        [DataMember(Order = 2)] public int Count { get; set; } = 0;

        [DataMember(Order = 3)] public string? Message { get; set; }
    }
}
