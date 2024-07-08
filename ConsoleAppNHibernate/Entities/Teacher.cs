namespace QLSVConsole.Entities
{
    public class Teacher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual DateTime Birthday { get; set; }
        public virtual IList<Class> Classes { get; set; } = new List<Class>();
    }
}
