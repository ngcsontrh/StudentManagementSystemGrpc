﻿using ProtoBuf.Grpc;
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
        Task<MultipleClassInfosReply> GetAllClassesInfo(Empty request, CallContext context = default);
    }

    [DataContract]
    public class ClassInfo
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; } = null!;

        [DataMember(Order = 3)]
        public string Subject { get; set; } = null!;
    }

    [DataContract]
    public class MultipleClassInfosReply
    {
        [DataMember(Order = 1)]
        public List<ClassInfo>? Classes { get; set; }

        [DataMember(Order = 2)]
        public int Count { get; set; } = 0;

        [DataMember(Order = 3)]
        public string? Message { get; set; }
    }
}