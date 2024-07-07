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
    public class OperationReply
    {
        [DataMember(Order = 1)] public bool Success { get; set; } = false;

        [DataMember(Order = 2)] public string? Message { get; set; }
    }
}
