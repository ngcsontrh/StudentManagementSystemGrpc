using System.ComponentModel.DataAnnotations;

namespace Client.Blazor.Models
{
    public class StudentForm
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public DateTime Birthday { get; set; } = DateTime.Now;

        [Required]
        public int ClassId { get; set; }

        public string? ClassName { get; set; }
    }
}
