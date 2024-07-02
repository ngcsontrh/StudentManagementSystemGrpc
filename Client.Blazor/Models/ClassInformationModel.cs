using System.Runtime.Serialization;

namespace Client.Blazor.Models
{
    public class ClassInformationModel
    {        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
