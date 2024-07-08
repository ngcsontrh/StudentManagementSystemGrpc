using Server.Entities;
using Shared;

namespace Server.DTOs
{
    public class PageViewDTO<T> where T : class
    {
        public int Total { get; set; }
        public List<T>? Students { get; set; }
    }
}
