using ProtoBuf.Grpc;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        Task<OperationReply> CreateAsync(StudentProfile request, CallContext context = default);

        [OperationContract]
        Task<OperationReply> UpdateAsync(StudentProfile request, CallContext context = default);

        [OperationContract]
        Task<OperationReply> DeleteAsync(IdRequest request, CallContext context = default);

        [OperationContract]
        Task<StudentProfileReply> GetProfileAsync(IdRequest request, CallContext context = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetAllProfilesAsync(Empty request, CallContext context = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetPaginationAsync(PaginationRequest request, CallContext callContext = default);

        [OperationContract]
        Task<StudentAgeChart> GetStudentAgeChartAsync(IdRequest classIdRequest, CallContext callContext = default);
    }

    [DataContract]
    public class StudentAge
    {
        [DataMember(Order = 1)] public int Age { get; set; }
        [DataMember(Order = 2)] public int NumberOfStudent { get; set; }
    }

    [DataContract]
    public class StudentCount
    {
        [DataMember(Order = 1)] public int Total { get; set; }
    }

    [DataContract]
    public class StudentAgeChart
    {
        [DataMember(Order = 1)] public List<StudentAge> ChartData { get; set; } = null!;
    }

    [DataContract]
    public class PaginationRequest
    {
        [DataMember(Order = 1)] public int? Id { get; set; }
        [DataMember(Order = 2)] public string? Name { get; set; }
        [DataMember(Order = 3)] public string? Address { get; set; }
        [DataMember(Order = 4)] public int? ClassId { get; set; }
        [DataMember(Order = 5)] public DateTime? StartDate { get; set; }
        [DataMember(Order = 6)] public DateTime? EndDate { get; set; }
        [DataMember(Order = 7)] public int PageNumber { get; set; }
        [DataMember(Order = 8)] public int PageSize { get; set; }
    }

    [DataContract]
    public class StudentProfileReply
    {
        [DataMember(Order = 1)] public StudentProfile? Student { get; set; }
        [DataMember(Order = 2)] public string? Message { get; set; }
    }

    [DataContract]
    public class MultipleStudentProfilesReply
    {
        [DataMember(Order = 1)] public List<StudentProfile>? Students { get; set; }
        [DataMember(Order = 2)] public int Count { get; set; } = 0;
        [DataMember(Order = 3)] public string? Message { get; set; }
    }

    [DataContract]
    public class StudentProfile
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string FullName { get; set; } = null!;
        [DataMember(Order = 3)] public DateTime Birthday { get; set; }
        [DataMember(Order = 4)] public string Address { get; set; } = null!;
        [DataMember(Order = 5)] public int ClassId { get; set; }
        [DataMember(Order = 6)] public string ClassName { get; set; } = null!;
        [DataMember(Order = 7)] public string ClassSubject { get; set; } = null!;
        [DataMember(Order = 8)] public int TeacherId { get; set; }
        [DataMember(Order = 9)] public string TeacherFullName { get; set; } = null!;
        [DataMember(Order = 10)] public DateTime TeacherBirthday { get; set; }
    }
}
