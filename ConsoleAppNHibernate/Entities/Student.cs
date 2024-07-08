using System.ComponentModel.DataAnnotations;

namespace QLSVConsole.Entities
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual DateTime Birthday { get; set; }
        public virtual string Address { get; set; } = null!;
        public virtual Class Class { get; set; } = null!;
    }
}
