namespace QLSVConsole.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = null!;
        public Class Class { get; set; } = null!;

    }
}
