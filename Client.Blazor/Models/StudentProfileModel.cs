using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Client.Blazor.Models
{
    public class StudentProfileModel
    {
        public int Id { get; set; }
        [Required] public string FullName { get; set; } = null!;
        [Required] public DateTime Birthday { get; set; }
        [Required] public string Address { get; set; } = null!;
        [Required] public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public string ClassSubject { get; set; } = null!;
        public int TeacherId { get; set; }
        public string TeacherFullName { get; set; } = null!;
        public DateTime TeacherBirthday { get; set; }
    }
}
