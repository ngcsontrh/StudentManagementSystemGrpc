namespace QLSVConsole.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
    }
}
