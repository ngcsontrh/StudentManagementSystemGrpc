namespace QLSVConsole.Entities
{
    public class Class
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual string Subject { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual IList<Student> Students { get; set; } = new List<Student>();
    }
}
