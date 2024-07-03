using ProtoBuf.Grpc;
using Shared.Models;
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
        Task<OperationReply> CreateAsync(CreateStudentRequest request, CallContext context = default);

        [OperationContract]
        Task<OperationReply> UpdateAsync(UpdateStudentRequest request, CallContext context = default);

        [OperationContract]
        Task<OperationReply> DeleteAsync(IdRequest request, CallContext context = default);

        [OperationContract]
        Task<StudentDetailsReply> GetDetailsAsync(IdRequest request, CallContext context = default);

        [OperationContract]
        Task<StudentProfileReply> GetProfileAsync(IdRequest request, CallContext context = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetAllProfilesAsync(Empty request, CallContext context = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetWithPaginationAsync(PaginationRequest request, CallContext context = default);

        [OperationContract] // bug
        Task<MultipleStudentProfilesReply> SearchStudentAsync(SearchRequest request, CallContext callContext = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetProfileByNameAsync(NameRequest request, CallContext callContext = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetProfileByAddressAsync(AddressRequest request, CallContext callContext = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetProfileByClassAsync(IdRequest request, CallContext callContext = default);

        [OperationContract]
        Task<MultipleStudentProfilesReply> GetProfileByDateAsync(DateRequest request, CallContext callContext = default);
    }

    // bug
    [DataContract]
    public class SearchRequest
    {
        [DataMember(Order = 1)] public int? Id { get; set; }
        [DataMember(Order = 2)] public string? Name { get; set; }
        [DataMember(Order = 3)] public DateTime? StartDate { get; set; }
        [DataMember(Order = 4)] public DateTime? EndDate { get; set; }
        [DataMember(Order = 5)] public string? Address { get; set; }
        [DataMember(Order = 6)] public int? ClassId { get; set; }
    }

    [DataContract]
    public class AddressRequest
    {
        [DataMember(Order = 1)] public string Address { get; set; } = null!;
    }

    [DataContract]
    public class CreateStudentRequest
    {
        [DataMember(Order = 1)] public string FullName { get; set; } = null!;
        [DataMember(Order = 2)] public DateTime Birthday { get; set; }
        [DataMember(Order = 3)] public string Address { get; set; } = null!;
        [DataMember(Order = 4)] public int ClassId { get; set; }
    }

    [DataContract]
    public class StudentDetailsReply
    {
        [DataMember(Order = 1)] public StudentDetails? Student { get; set; }
        [DataMember(Order = 2)] public string? Message { get; set; }
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
    public class StudentDetails
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

    [DataContract]
    public class StudentProfile
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string FullName { get; set; } = null!;
        [DataMember(Order = 3)] public DateTime Birthday { get; set; }
        [DataMember(Order = 4)] public string Address { get; set; } = null!;
        [DataMember(Order = 5)] public int ClassId { get; set; }
        [DataMember(Order = 6)] public string ClassName { get; set; } = null!;
    }

    [DataContract]
    public class UpdateStudentRequest
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public string FullName { get; set; } = null!;
        [DataMember(Order = 3)] public DateTime Birthday { get; set; }
        [DataMember(Order = 4)] public string Address { get; set; } = null!;
        [DataMember(Order = 5)] public int ClassId { get; set; }
    }
}
