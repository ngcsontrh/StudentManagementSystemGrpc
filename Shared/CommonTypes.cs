using System.Runtime.Serialization;

namespace Shared
{
    [DataContract]
    public class Empty { }

    [DataContract]
    public class IdRequest
    {
        [DataMember(Order = 1)] public int Id { get; set; }
    }

    [DataContract]
    public class NameRequest
    {
        [DataMember(Order = 1)] public string Name { get; set; } = null!;
    }

    [DataContract]
    public class DateRequest
    {
        [DataMember(Order = 1)] public DateTime DateStart { get; set; }
        [DataMember(Order = 2)] public DateTime DateEnd { get; set; }
    }

    [DataContract]
    public class OperationReply
    {
        [DataMember(Order = 1)] public bool Success { get; set; } = false;

        [DataMember(Order = 2)] public string? Message { get; set; }
    }

    [DataContract]
    public class PaginationRequest
    {
        [DataMember(Order = 1)] public int PageNumber { get; set; }

        [DataMember(Order = 2)] public int PageSize { get; set; }
    }
}
